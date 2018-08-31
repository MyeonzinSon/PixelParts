using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Queue<GameObject> particlePool = new Queue<GameObject>();
    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }
    void Start(){
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