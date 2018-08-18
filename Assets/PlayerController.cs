using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    Animator anim;
    bool isOnGround;
	
	void Start () {
        anim = GetComponent<Animator>();
		
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
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, +speed * Time.deltaTime, 0);
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
