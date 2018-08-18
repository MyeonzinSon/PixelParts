using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD

=======
>>>>>>> e42081b21d8fa4edf631cad9eeb7b8f7d2f18774
public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
<<<<<<< HEAD
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(jumpForce * Vector2.up);
        }
        else if(Input.GetKey(KeyCode.Z))
        {

        }

    }
=======
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
>>>>>>> e42081b21d8fa4edf631cad9eeb7b8f7d2f18774
}
