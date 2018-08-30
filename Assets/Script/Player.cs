using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHP;
    public float speed;
    public float jumpForce;
    public GameObject weaponGO;
    public Transform hand;
    int hp;
    int _direction;
    public int Direction{
        get {
            return _direction;
        }
        private set {
            if (value == 1){
                _direction = 1;
                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            } else if (value == -1) {
                _direction = -1;
                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            } else if (value == 0){
                _direction = 0;
            }
        }
    }
    bool isOnGround;
    AnimatorTriggerBool isJumping;
    AnimatorTriggerBool isWalking;
    Animator anim;
    Rigidbody2D rb;
    Weapon weapon;
	
	void Start () {
        hp = maxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = new AnimatorTriggerBool(anim, "jump");
        isWalking = new AnimatorTriggerBool(anim, "walk");
        if (weaponGO != null){
            EquipWeapon(weaponGO);
        }
	}
	void Update () {
        
        Direction = 0;

		InputKey();

        if (Direction == 0 && isWalking){
            isWalking.Set(false);
        }
        if ((Direction == 1 || Direction == -1) && !isWalking){
            isWalking.Set(true);
        }

        transform.Translate(Direction *speed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        isOnGround = true;
        isJumping.Set(false);
    }
    void InputKey(){
        if (Input.GetKey(KeyCode.A)) {
            Direction = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            Direction = 1;
        }
        if(Input.GetKey(KeyCode.W) && isOnGround) {
            rb.AddForce(jumpForce * Vector2.up);
            isOnGround = false;
            isJumping.Set(true);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (weapon != null){
                weapon.Attack();
            }
        }
    }
    public void TakeDamage(int damage){
        hp -= damage;
        Debug.Log("HP : "+hp);

        if (hp <= 0){
            Debug.Log("You are dead!");
        }
    }
    public void EquipWeapon(GameObject wp){
        Weapon weaponComp = wp.GetComponent<Weapon>();
        if (weaponComp != null){
            if (wp != weaponGO) {
                Destroy(weaponGO);
            }
            weaponGO = wp;
            weapon = weaponComp;
        }
    }
}