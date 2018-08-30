using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int attackPoint;
    public float attackPeriod;
    public float attackTime = 0.2f;
    public float knockBack = 1;
    bool canAttack = true;
    bool isAttacking = false;
    float timer;
    float cooldown;
    float cooldownReduce = 1;
    Player player;
    ContactFilter2D filter = new ContactFilter2D();
    void Start(){
        player = GameManager.Instance.player;
        transform.SetParent(player.hand);
        transform.localPosition = Vector3.zero;
        filter.SetLayerMask(LayerMask.GetMask("Monster"));
    }
	void Update () {
        
		if (isAttacking) {
            if (timer < attackTime) {
                timer += Time.deltaTime;
                float rate = timer/attackTime;

                Quaternion quat = Quaternion.identity;
                quat.eulerAngles = new Vector3(0, 0, -135 * player.Direction * rate);
                transform.rotation = quat;
            } else {
                isAttacking = false;
                transform.rotation = Quaternion.identity;
            }
        }
        if (!canAttack) {
            cooldown -= Time.deltaTime / cooldownReduce;
            if (cooldown <= 0) {
                canAttack = true;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other){
        DealDamage(other);
    }
    void DealDamage(Collider2D other){
        if (other != null && other.gameObject.GetComponent<Monster>() != null && isAttacking){
            other.gameObject.GetComponent<Monster>().TakeDamage(attackPoint);
        }
    }
    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            isAttacking = true;
            timer = 0;
            cooldown = attackPeriod;

            Collider2D[] results = new Collider2D[255];
            int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, results);
            results.All(col => {
                DealDamage(col);
                return true;
            });
        } else {
            Debug.Log("Weapon is on cooldown! Remain time = " + cooldown);
        }
    }

    public void SetCooldownReduce(float value) {
        cooldownReduce = value;
    }
}
