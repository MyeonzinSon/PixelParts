using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    
    public float speed = 20;
    public int damage = 10;
    GameObject player;
    Animator anim;
    bool isMoving = true;
    void Start () {
        player = GameManager.Instance.playerGO;
        anim = GetComponent<Animator>();

        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        Destroy(gameObject, 10);
    }
	
	void Update() {
        if (isMoving) {
            transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null) {
            coll.GetComponent<Player>().TakeDamage(damage);
            isMoving = false;
            anim.SetTrigger("explode");
            Destroy(gameObject,0.25f);
        }
    }
}
