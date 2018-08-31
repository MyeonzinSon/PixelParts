using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager> ();
                if (_instance == null) {
                    GameObject obj = new GameObject ();
                    obj.name = typeof(GameManager).Name;
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
 
    void Awake () {
        if (_instance == null) {
            _instance = this as GameManager;
            DontDestroyOnLoad (this.gameObject);
        } else {
            Destroy (gameObject);
        }

        if (playerGO == null){
            playerGO = FindObjectOfType<Player>().gameObject;
        }
    }
    public GameObject playerGO;
    public GameObject particlePrefab;
    public GameObject[] weaponPrizes;
    public GameObject[] companionPrizes;
    int weaponPrizesIndex;
    int companionPrizesIndex;
    Queue<GameObject> particlePool = new Queue<GameObject>();
    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }
    public void MoveScene(string scene){
        DontDestroyOnLoad(playerGO);
        player.transform.position = 32 * Vector2.left;
        int i = 0;
        foreach(var comp in player.companions){
            comp.gameObject.transform.position = (32 + i++) * Vector2.left;
        }
        SceneManager.LoadScene(scene);
    }	
    public void NextPirze(bool doWeaponPrize = false){
        if(doWeaponPrize){
            NextWeaponPrize(false);
            return;
        }

        if(companionPrizesIndex < companionPrizes.Length && weaponPrizesIndex < weaponPrizes.Length){
            if (Random.value > (float)weaponPrizes.Length / (float)(companionPrizes.Length + weaponPrizes.Length)) { 
                NextCompanionPrize();
            } else { 
                NextWeaponPrize();
            }
        } else {
            if(companionPrizesIndex == companionPrizes.Length) {
                NextWeaponPrize();
            } else if (weaponPrizesIndex == weaponPrizes.Length) {
                NextCompanionPrize();
            } else {
                Debug.Log("There is no prizes now!");
            }
        }
	}
    void NextCompanionPrize(bool fallFromAbove = false){
        if (!(companionPrizesIndex < companionPrizes.Length)){
            companionPrizesIndex--;
        }
        var go = Instantiate(companionPrizes[companionPrizesIndex++]);
		if (fallFromAbove){
			go.transform.position = GameManager.Instance.playerGO.transform.position + 2 * Vector3.up;
		} else {
			go.transform.position = GameManager.Instance.playerGO.transform.position + 2 * Vector3.right;
		}
    }
    void NextWeaponPrize(bool fallFromAbove = true){
        if (!(weaponPrizesIndex < weaponPrizes.Length)){
            weaponPrizesIndex--;
        }
        var go = Instantiate(weaponPrizes[weaponPrizesIndex++]);
		if (fallFromAbove){
			go.transform.position = GameManager.Instance.playerGO.transform.position + 2 * Vector3.up;
		} else {
			go.transform.position = GameManager.Instance.playerGO.transform.position + 2 * Vector3.right;
		}
    }
    GameObject NewParticle(){
        var newGO = Instantiate(particlePrefab, transform);
        newGO.SetActive(false);
        return newGO;
    }
    public void ShowParticle(Vector2 position){
        GameObject go;
        if (particlePool.Count > 0){
            go = particlePool.Dequeue();
        } else {
            go = NewParticle();
        }
        go.transform.position = new Vector3(position.x, position.y, -3);
        Vector3 scale = player.transform.localScale;
        go.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        go.SetActive(true);
    }
    public void EnqueueParticle(GameObject go){
        particlePool.Enqueue(go);
    }

}