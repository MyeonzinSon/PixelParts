using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    public GameObject playerGO;
    public Player player{
        get{
            return playerGO.GetComponent<Player>();
        }
    }

}