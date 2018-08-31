using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    
    public float speed = 5;
    GameObject player;
    void Start () {
        player = GameManager.Instance.playerGO;
    }
	
	void Update() {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        speed += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null) {
            coll.GetComponent<Player>().GetHealed();
            Destroy(gameObject);
        }
    }
}
