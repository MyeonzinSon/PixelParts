using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    Animator anim;
    bool isOnGround;
    Rigidbody2D rb;
    public weapon wp;
	
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<weapon>();
     
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(+speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            rb.AddForce(jumpForce * Vector2.up);
            isOnGround = false;
            anim.SetBool("jump", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            wp.Attack();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        anim.SetBool("jump", false);
    }
}
