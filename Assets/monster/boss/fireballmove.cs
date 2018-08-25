using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballmove : MonoBehaviour {
    GameObject player;
    public Animator anim;
    bool move = true;
    // Use this for initialization
    void Start () {
       
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
	
	// Update is called once per frame
	void Update() {
        
        if (move  == true)
        {
            transform.Translate(0.2f, 0, 0);
        }
       
        Destroy(gameObject,20f);
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        anim.SetBool("a", true);
        Destroy(gameObject,1f);
        move = false;
    }
}
