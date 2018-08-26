using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {

    public GameObject maxBar;
    public GameObject currentBar;

    int maxHP;
    int currentHP;

    public void UpdateHP(int max, int current)
    {
        maxHP = max;
        currentHP = current;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rate = (float)currentHP / maxHP;

        currentBar.transform.localScale = new Vector3(rate, 1, 1);
        currentBar.transform.position = new Vector3(-0.25f * (1 - rate), 0, 0);
	}
}
