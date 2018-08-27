using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    Rigidbody2D rb;

    public int maxHP;
    public int attackPower;
    public int jumpforce;
    public float walkForce;
    public float walkPeriod;
    public float walkSpeed;
    
    int HP;
    float walkTimer;
    int _direction;
    int Direction {
        get{
            return _direction;
        }
        set{
            _direction = value;
            if (_direction == 1 || _direction == -1){
                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(_direction * -Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
    }
    bool isJumping = false;
    GameObject player;
    Vector2 delta;
    void Start() {
        player = GameManager.instance.player;
        rb = GetComponent<Rigidbody2D>();  
        Direction = -1;
        HP = maxHP;
    }

    void OnCollisionStay2D(Collision2D coll) {
        if (isJumping == true && this.rb.velocity.y < 0.1f) {
            rb.AddForce(transform.up * jumpforce);
        }
    }

    void Update() {
        delta = player.transform.position - transform.position;
        float distance = delta.magnitude;

        if (distance < 15) {
            isJumping = true;

            if (delta.x > 0) {
                Direction = 1;
            } else {
                Direction = -1;
            }
        } else {
            isJumping = false;
            walkTimer += Time.deltaTime;

            if(walkTimer >= walkPeriod){
                Direction *= -1;
                walkTimer -= walkPeriod;
            }
        }

        if (-walkSpeed < rb.velocity.x && rb.velocity.x < walkSpeed) {
            rb.AddForce(new Vector3(Direction * walkForce, 0, 0));
        }
    }

    public void TakeDamage(int damage){
        HP -= damage;
        if (HP <= 0){
            Destroy(gameObject);
        }
    }
}
