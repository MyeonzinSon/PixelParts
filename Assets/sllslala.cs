using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sllslala : MonoBehaviour {
   
    Rigidbody2D ridgid;
    public int jumpforce;
    public float walkforce;
    public int walktime = 0;
    public int walkTimeLimit = 50;
    public float speedLimit;
    int key = 1;
    public Vector2 direction;
    bool jump = false;
    // Use this for initialization
    void Start () {
        this.ridgid = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
   
        void OnCollisionStay2D(Collision2D coll)
        {
            if (jump == true && this.ridgid.velocity.y < 0.1f)
            {
                this.ridgid.AddForce(transform.up * this.jumpforce);
            }
        }
    
    void Update () {

        direction = new Vector2(key, 0);
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, 10.0f);

        
        if (hit.collider != null)
        {
            Debug.Log("hit.collider != null");
            if (hit.collider.gameObject.tag == "Player") jump = true;
        }

        
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

        if (ridgid.velocity.x < speedLimit && -speedLimit < ridgid.velocity.x)
        {
            ridgid.AddForce(new Vector3(key * walkforce, 0, 0));
        }
     
        

    }
}
