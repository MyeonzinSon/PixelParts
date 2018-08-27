using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set;}
    void SetSingleton(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public GameObject player;

    void Awake(){
        SetSingleton();
    }
}