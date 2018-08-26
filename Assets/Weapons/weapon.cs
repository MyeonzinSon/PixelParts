using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour {

    public int attackPoint;
    public float attackPeriod;
    bool canAttack;
    public float boxsizeX;
    public float boxsizeY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;

            RaycastHit2D[] result = Physics2D.BoxCastAll(transform.position, new Vector2(boxsizeX, boxsizeY), 0, Vector2.zero);
            for (int i = 0; i < result.Length; i++)
            {
                result[i].collider.gameObject.GetComponent<Slime>().Damage(attackPoint);
            }

            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackPeriod);
        canAttack = true;
    }
}
