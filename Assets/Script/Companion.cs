using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Companion : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    public int jumpforce;
    public float walkForce;
    public float walkSpeed;
    public int walkBehind;
    public GameObject heart;
    public bool flipSprite = false;
    
    bool isRescued;
    bool isOnGround;
    float jumpTime;
    int _direction;
    public int Direction {
        get{
            return _direction;
        }
        private set{
            _direction = value;
            if (delta.x + walkBehind > 0){
                sr.flipX = !(true ^ flipSprite);
            } else if (delta.x + walkBehind < 0){
                sr.flipX = !(false ^ flipSprite);
            }
        }
    }
    AnimatorTriggerBool isJumping;
    AnimatorTriggerBool isWalking;
    GameObject player;
    HPBar hpBar;
    Vector2 delta;
    MapManager map;
    void Start() {
        player = GameManager.Instance.playerGO;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  
        rb.Sleep();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        Direction = -1;
        isJumping = new AnimatorTriggerBool(anim, "jump", false);
        isWalking = new AnimatorTriggerBool(anim, "walk", false);
    }
    void Rescue(){
        isRescued = true;
        rb.WakeUp();
        anim.enabled = true;
        GameManager.Instance.player.RescueCompanion(this);
        DontDestroyOnLoad(gameObject);
    }
    public void ShootHeart(){
        Instantiate(heart, transform.position, Quaternion.identity);
    }
    void OnCollisionEnter2D(Collision2D coll){
        if (!isRescued && coll.gameObject.GetComponent<Player>() != null){
            Rescue();
        }
    }
    void OnCollisionStay2D(Collision2D coll) {
        if (isJumping == true && this.rb.velocity.y < 0.1f) {
            rb.AddForce(transform.up * jumpforce);
        }
    }

    void Update() {
        if(!isRescued) return;
        
        delta = player.transform.position + walkBehind * Vector3.left - transform.position;
        float distance = delta.magnitude;

        if (distance < 1){
            isWalking.Set(true);

            if (delta.x > 0) {
                Direction = -1;
            } else {
                Direction = 1;
            }
        } else if (distance > 3){
            isWalking.Set(true);
            isJumping.Set(true);
            isOnGround = false;

            if (delta.x > 0) {
                Direction = 1;
            } else {
                Direction = -1;
            }
        } else {
            isWalking.Set(false);
            isJumping.Set(false);
            
            if (delta.x > 0) {
                Direction = 1;
            } else {
                Direction = -1;
            }
            Direction = 0;
        }

        if (-walkSpeed < rb.velocity.x && rb.velocity.x < walkSpeed) {
            rb.AddForce(new Vector3(Direction * walkForce, 0, 0));
        }

        if (isJumping) {
            jumpTime += Time.deltaTime;
            if (jumpTime > 1){
                isJumping.Set(false);
            }
        } else {
            jumpTime = 0;
        }

    }
}
