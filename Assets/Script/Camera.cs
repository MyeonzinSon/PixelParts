using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    GameObject player;
    
    void Start () {
	    player = GameManager.Instance.playerGO;
    }
    void Update () {
        float x = player.transform.position.x;
        if (x > 32) {
            x = 32;
        }
        if (x < -32) {
            x = -32;
        }
        float y = player.transform.position.y;
        if (y > 0.4) {
            y = 0.4f;
        }
        if (y < -0.4) {
            y = -0.4f;
        }
        transform.position = new Vector3(x, y, transform.position.z);
    }

}
