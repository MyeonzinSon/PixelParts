using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {

    public GameObject maxBar;
    public GameObject currentBar;

    Monster parentMonster;
    int maxHP;
    int currentHP;

	void Start () {
        parentMonster = GetComponentInParent<Monster>();
		if (parentMonster != null){
            parentMonster.ConnectWithHPBar(this);
        }
	}

    public void SetMaxHP(int max){
        maxHP = max;
        SetCurrentHP(max);
    }

    public void SetCurrentHP(int current){
        currentHP = current;
        UpdateHP();
    }
    public void FlipHP(int direction){
        Vector3 scale = currentBar.transform.localScale;
        currentBar.transform.localScale = new Vector3(direction * -scale.x, scale.y, scale.z);
    }
    public void UpdateHP()
    {
        float rate = (float)currentHP / maxHP;
        if (rate < 0) {
            rate = 0;
        }

        currentBar.transform.localScale = new Vector3(rate, 1, 1);
        currentBar.transform.localPosition = new Vector3(-0.25f * (1 - rate), 0, -0.1f);
    }
}
