using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
    public Animator anim;
    Rigidbody2D ridgid;
    bool jump;
    public int jumpforce;
    public float walkforce;
    float distance;
    public int walktime = 0;
    public int walkTimeLimit = 50;
    public float speedLimit;
    public int 
    int key = 1;
    public Vector2 direction;
    bool jump1 = false;
    GameObject player;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        this.ridgid = GetComponent<Rigidbody2D>();
        jump = false;
	}

    // Update is called once per frame
   
        void OnCollisionStay2D(Collision2D coll)
        {
            if (jump1 == true && this.ridgid.velocity.y < 0.1f)
            {
                this.ridgid.AddForce(transform.up * this.jumpforce);
            }
        }
    
    void Update () {
        Vector2 delta = transform.position - player.transform.position;
        distance = delta.magnitude;
        Debug.Log(distance);
        float deltaX = delta.x;
        if (distance < 15)
        {
            jump1 = true;
            anim.SetBool("jump", true);
            if (deltaX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                key = -1;
            }
            if (deltaX < 0)
            {
                transform.localScale = new Vector3(1 , 1, 1);
                key = 1;
            }

        }
        else
        {
            jump1 = false;
            anim.SetBool("jump", false);
            if (walktime >= walkTimeLimit)
            {
                key = 1;
            }
            if (walktime < -walkTimeLimit)
            {
                key = -1;
            }
            walktime -= key;


            if (key != 0)
            {
                transform.localScale = new Vector3(key, 1, 1);
            }
        }
            if (ridgid.velocity.x < speedLimit && -speedLimit < ridgid.velocity.x)
            {
                ridgid.AddForce(new Vector3(key * walkforce, 0, 0));
            }


        

    }
}
