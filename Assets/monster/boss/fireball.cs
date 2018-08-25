using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {

    public GameObject fireBall;
	// Use this for initialization
	void Start () {
        StartCoroutine(Shoot());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(fireBall, transform.position, Quaternion.identity);
        }
    }
}
