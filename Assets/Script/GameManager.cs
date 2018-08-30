using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    public GameObject playerGO;

    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }
    void Start(){
        if (playerGO == null){
            playerGO = FindObjectOfType<Player>().gameObject;
        }
    }

}