using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    GameObject player;
    public float horizontalLimit = 39;
    public float verticalLimit = 0.4f;
    void Start () {
	    player = GameManager.Instance.playerGO;
    }
    void Update () {
        float x = player.transform.position.x;
        if (x > horizontalLimit) {
            x = horizontalLimit;
        }
        if (x < -horizontalLimit) {
            x = -horizontalLimit;
        }
        float y = player.transform.position.y;
        if (y > verticalLimit) {
            y = verticalLimit;
        }
        if (y < -verticalLimit) {
            y = -verticalLimit;
        }
        transform.position = new Vector3(x, y, transform.position.z);
    }

}
