using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static GameManager _instance;
    static bool isAssassin;
    public static void ChooseCharacter(bool assassin){
        isAssassin = assassin;
    }
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
            if(isAssassin){
                var go = Instantiate(characterDict[0]);
                go.transform.position = 38 * Vector2.left;
                playerGO = go;
            } else {
                var go = Instantiate(characterDict[1]);
                go.transform.position = 38 * Vector2.left;
                playerGO = go;
            }
        }
    }
    public GameObject playerGO;
    public GameObject particlePrefab;
    public GameObject[] characterDict;
    public GameObject[] weaponPrizes;
    public GameObject[] companionPrizes;
    public List<Weapon> wastedWeapons;
    int weaponPrizesIndex;
    int companionPrizesIndex;
    Queue<GameObject> particlePool = new Queue<GameObject>();
    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }
    public void GameOver(){
        SceneManager.LoadScene("GameOver");
    }
    public void GameClear(){
        SceneManager.LoadScene("GameClear");
    }
    public void MoveScene(string scene){
        DontDestroyOnLoad(playerGO);
        foreach (var wp in wastedWeapons){
            if(wp.gameObject != null){
                Destroy(wp.gameObject);
            }
        }
        wastedWeapons = new List<Weapon>();
        player.transform.position = 32 * Vector2.left;
        int i = 0;
        foreach(var comp in player.companions){
            comp.gameObject.transform.position = (36 + i++) * Vector2.left;
        }
        SceneManager.LoadScene(scene);
    }	
    void Update(){
        if(Input.GetKeyDown(KeyCode.PageDown))
        {
            NextPrize();
        }
    }
    public void NextPrize(bool doWeaponPrize = false){
        if(doWeaponPrize){
            NextWeaponPrize();
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
    void NextCompanionPrize(){
        if (!(companionPrizesIndex < companionPrizes.Length)){
            companionPrizesIndex--;
        }
        var go = Instantiate(companionPrizes[companionPrizesIndex++]);
        go.transform.position = new Vector2(playerGO.transform.position.x + 2, -2);
    }
    void NextWeaponPrize(){
        if (!(weaponPrizesIndex < weaponPrizes.Length)){
            weaponPrizesIndex--;
        }
        var go = Instantiate(weaponPrizes[weaponPrizesIndex++]);
        go.transform.position = new Vector2(playerGO.transform.position.x + 2, -2);
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
        go.transform.localScale = new Vector3(-player.LastDirection * scale.x, scale.y, scale.z);
        go.SetActive(true);
    }
    public void EnqueueParticle(GameObject go){
        particlePool.Enqueue(go);
    }

}