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
                LastDirection = 1;
                sr.flipX = false;
                Vector2 pos = hand.localPosition;
                hand.localPosition = new Vector2(Mathf.Abs(pos.x), pos.y);
            } else if (value == -1) {
                _direction = -1;
                LastDirection = -1;
                sr.flipX = true;
                Vector2 pos = hand.localPosition;
                hand.localPosition = new Vector2(-Mathf.Abs(pos.x), pos.y);
            } else if (value == 0){
                _direction = 0;
            }
        }
    }
    public int LastDirection{
        get;
        private set;
    }
    bool isOnGround;
    AnimatorTriggerBool isJumping;
    AnimatorTriggerBool isWalking;
    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    Weapon weapon;
    HPBar hpBar;
	
	void Start () {
        GameManager.Instance.playerGO = gameObject;
        hp = maxHP;
        sr = GetComponent<SpriteRenderer>();
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

        rb.velocity = new Vector2(Direction * speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        isOnGround = true;
        isJumping.Set(false);
    }
    void InputKey(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            Direction = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            Direction = 1;
        }
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isOnGround) {
            rb.AddForce(jumpForce * Vector2.up);
            isOnGround = false;
            isJumping.Set(true);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftControl)) {
            if (weapon != null){
                weapon.Attack();
            }
        }
    }
    public void TakeDamage(int damage){
        hp -= damage;
        hpBar.SetCurrentHP(hp);

        if (hp <= 0){
            GameManager.Instance.GameOver();
        }
    }
    public void GetHealed(){
        hp += maxHP / 50;
        hpBar.SetCurrentHP(hp);

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
    public void ConnectWithHPBar(HPBar bar){
        hpBar = bar;
        hpBar.SetMaxHP(maxHP);
    }
}
