using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int attackPoint;
    public float attackPeriod;
    public float attackTime = 0.2f;
    public float knockBack = 1;
    public bool isMelee = true;
    bool canAttack = true;
    bool isAttacking = false;
    int _direction;
    int Direction{
        get{
            return _direction;
        }
        set{
            if (value == 1 || value == -1){
                _direction = value;
            }
        }
    }
    float timer;
    float cooldown;
    float cooldownReduce = 1;
    Player player;
    ContactFilter2D filter = new ContactFilter2D();
    void Start(){
        player = GameManager.Instance.player;
        transform.SetParent(player.hand);
        transform.localPosition = Vector3.zero + Vector3.back;
        filter.SetLayerMask(LayerMask.GetMask("Monster"));
        Direction = 1;
    }
	void Update () {
        Direction = player.Direction;
		if (isAttacking) {
            if (timer < attackTime) {
                timer += Time.deltaTime;
                float rate = timer/attackTime;

                if(isMelee){
                    Quaternion quat = Quaternion.identity;
                    quat.eulerAngles = new Vector3(0, 0, -135 * Direction * Tween.Pow(timer,0,attackTime,1));
                    transform.rotation = quat;
                }
            } else {
                isAttacking = false;
            }
        }
        if (!canAttack) {
            cooldown -= Time.deltaTime / cooldownReduce;
            if (cooldown <= 0) {
                cooldown = 0;
                canAttack = true;
            }
            if (!isAttacking && isMelee){
                Quaternion quat = Quaternion.identity;
                quat.eulerAngles = new Vector3(0, 0, -135 * Direction * Tween.Pow(cooldown, 0, (attackPeriod - attackTime), 1));
                transform.rotation = quat;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other){
        if(isMelee){
            DealDamageMelee(other);
        }
    }
    void DealDamageMelee(Collider2D other){
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
            if (isMelee){
                int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, results);
                results.All(col => {
                    DealDamageMelee(col);
                    return true;
                });
            }
        } else {
            Debug.Log("Weapon is on cooldown! Remain time = " + cooldown);
        }
    }

    public void SetCooldownReduce(float value) {
        cooldownReduce = value;
    }
}
