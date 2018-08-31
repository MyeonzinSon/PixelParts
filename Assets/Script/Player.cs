using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHP;
    public float speed;
    public float jumpForce;
    public int strength;
    public float attackPeriodReduce;
    public GameObject weaponGO;
    public Transform hand;
    public List<Companion> companions { get; private set; }
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
        GameManager.Instance.playerGO = gameObject;
        hp = maxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        companions = new List<Companion>();
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

        if (hp <= 0){
            Debug.Log("You are dead!");
        }
    }
    public void GetHealed(){
        hp += maxHP / 50;

        if (hp >= maxHP){
            hp = maxHP;
        }
    }
    public void EquipWeapon(GameObject wp){
        Weapon weaponComp = wp.GetComponent<Weapon>();
        if (weaponComp != null){
            if (weaponGO != null) {
                ThrowOldWeapon(weaponGO);
            }
            weaponGO = wp;
            weapon = weaponComp;
        }
    }
    void ThrowOldWeapon(GameObject wpGO){
        wpGO.GetComponent<Weapon>().Unequip();
        var wprb = wpGO.AddComponent<Rigidbody2D>();
        wprb.AddForce(jumpForce * Vector2.up);
    }
    public void RescueCompanion(Companion comp){
        companions.Add(comp);
    }
    public void AskForCheer(){
        companions.All(comp => {
            comp.ShootHeart();
            return true;
        });
    }
}
