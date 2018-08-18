using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    Animator anim;
    bool isOnGround;
    Rigidbody2D rb;
	
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
     
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(jumpForce * Vector2.up);
            anim.SetBool("jump", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        anim.SetBool("jump", false);
    }
}
