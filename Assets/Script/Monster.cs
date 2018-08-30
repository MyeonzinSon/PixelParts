using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    public int maxHP;
    public int attackPower;
    public float attackRange = 1.5f;
    public float chaseRange = 5;
    public int jumpforce;
    public float walkForce;
    public float walkPeriod;
    public float walkSpeed;
    
    int HP;
    float walkTimer;
    int _direction;
    public int Direction {
        get{
            return _direction;
        }
        private set{
            _direction = value;
            if (_direction == 1){
                sr.flipX = true;
            } else if (_direction == -1){
                sr.flipX = false;
            }
        }
    }
    AnimatorTriggerBool isJumping;
    AnimatorTriggerBool isAttacking;
    GameObject player;
    HPBar hpBar;
    Vector2 delta;
    void Start() {
        player = GameManager.Instance.playerGO;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();
        Direction = -1;
        HP = maxHP;
        isJumping = new AnimatorTriggerBool(anim, "jump", false);
        isAttacking = new AnimatorTriggerBool(anim, "attack", false);
    }

    void OnCollisionStay2D(Collision2D coll) {
        if (isJumping == true && this.rb.velocity.y < 0.1f) {
            rb.AddForce(transform.up * jumpforce);
        }
    }

    void Update() {
        delta = player.transform.position - transform.position;
        float distance = delta.magnitude;

        if (distance < attackRange){
            isAttacking.Set(true);
        } else {
            isAttacking.Set(false);
        }
        if (distance < chaseRange) {
            isJumping.Set(true);

            if (delta.x > 0) {
                Direction = 1;
            } else {
                Direction = -1;
            }
        } else {
            isJumping.Set(false);
            walkTimer += Time.deltaTime;

            if(walkTimer >= walkPeriod){
                Direction *= -1;
                walkTimer -= walkPeriod;
            }
        }

        if (-walkSpeed < rb.velocity.x && rb.velocity.x < walkSpeed) {
            rb.AddForce(new Vector3(Direction * walkForce, 0, 0));
        }

        Cheat();
    }
    void Cheat(){
        if (Input.GetKeyDown(KeyCode.H)){
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage){
        HP -= damage;
        hpBar.SetCurrentHP(HP);

        if (HP <= 0){
            float deathDuration = 0;
            if(anim != null && anim.parameters.Any(a => a.name == "die")) {
                anim.SetTrigger("die");
                deathDuration = 0.5f;
            }
            Destroy(gameObject, deathDuration);
        }
    }

    public void ConnectWithHPBar(HPBar bar){
        hpBar = bar;
        hpBar.SetMaxHP(maxHP);
    }
}
