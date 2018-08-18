using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
