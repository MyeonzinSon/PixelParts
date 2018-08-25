using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {
    float distance;
    public GameObject fireBall;
    bool shoot;
    bool cooldown;
    GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        cooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 delta = transform.position - player.transform.position;
        distance = delta.magnitude;
        Debug.Log(distance);
        float deltaX = delta.x;
        if (distance < 50)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }

        if (shoot && !cooldown)
        {
            cooldown = true;
            Instantiate(fireBall, transform.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }

    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
    }
}
