using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet : MonoBehaviour
{

    Rigidbody2D ridgid;

    public int jumpforce;
    public float walkforce;
    float distance;
    public int walktime = 0;
    public int walkTimeLimit = 50;
    public float speedLimit;
    int key = 1;
    public Vector2 direction;
    bool jump1 = false;
    GameObject player;

    int stopCount;
    float xPast;
    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("player");
        this.ridgid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame

    void OnCollisionStay2D(Collision2D coll)
    {
        if (jump1 == true && this.ridgid.velocity.y < 0.1f)
        {
            this.ridgid.AddForce(transform.up * this.jumpforce);
        }
    }

    void Update()
    {
        if(Mathf.Abs(xPast - transform.position.x) < 0.01f)
        {
            stopCount += 1;
            if (stopCount > 15)
            {
                if (jump1 == true && this.ridgid.velocity.y < 0.1f)
                {
                    this.ridgid.AddForce(transform.up * this.jumpforce);
                }
                stopCount = 0;
            }
        }
        else
        {
            stopCount = 0;
        }

        Vector2 delta = transform.position - player.transform.position;
        distance = delta.magnitude;
        Debug.Log(distance);
        float deltaX = delta.x;
        if (distance > 10)
        {
            jump1 = true;

            if (deltaX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                key = -1;
            }
            if (deltaX < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                key = 1;
            }

        }
      
        
           // jump1 = false;

           
            if (key != 0)
            {
                transform.localScale = new Vector3(key, 1, 1);
            }
        
        if (ridgid.velocity.x < speedLimit && -speedLimit < ridgid.velocity.x)
        {
            ridgid.AddForce(new Vector3(key * walkforce, 0, 0));
        }



        xPast = transform.position.x;
    }
}
