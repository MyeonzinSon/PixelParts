using System.Collections;
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

    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }
    void Start(){
    }

}