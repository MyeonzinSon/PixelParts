﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {
    public Animator anim;
    Rigidbody2D ridgid;
    bool jump;
    public float jumpforce;
    public float walkforce;
    float distance;
    float distance1;

    public float speedLimit;
    int key;
    float walktime;
    float walkTimeLimit;

    public Vector2 direction;
    bool jump1 = false;
    GameObject player;
    GameObject thing;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        thing = GameObject.Find("thing");
        player = GameObject.Find("player");
        this.ridgid = GetComponent<Rigidbody2D>();
        jump = false;
    }

    void Update()
    {
        Vector2 delta1 = transform.position - thing.transform.position;
        distance1 = delta1.magnitude;
        Debug.Log(distance1);
       


        Vector2 delta2 = transform.position - player.transform.position;
        distance = delta2.magnitude;
        Debug.Log(distance);
        float deltaX = delta2.x;
        if (distance < 15)
            if (distance1 < 18)
            {
                {
                    speedLimit = 2;
                    jump1 = true;
                    anim.SetBool("idil", false);
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
            }
            else
            {
                speedLimit = 0;
                anim.SetBool("idil", true);
            }
        if (ridgid.velocity.x < speedLimit && -speedLimit < ridgid.velocity.x)
        {
            ridgid.AddForce(new Vector3(key * walkforce, 0, 0));
        }




    }
}
