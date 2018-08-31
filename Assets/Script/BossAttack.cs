using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    public GameObject fireBall;
    public GameObject blueSlime;
    List<GameObject> list;
    MapManager door;

    Monster boss;
    public void StartPattern(){
        boss = GetComponent<Monster>();
        list = new List<GameObject>();
        StartCoroutine(Pattern());
    }
    public void DestroyAll(){
        list.All(go => {
            if (go != null){
                Destroy(go);
            } 
            return true;
        });
    }
	
    IEnumerator Pattern(){
        while (true){
            if (boss.shootFireBall){
                yield return ShootFireBall();
            } else {
                yield return SpawnBlueSlime();
            }
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator ShootFireBall(){
        for (int i = 0; i < 5; i++){
            GameObject fb = Instantiate(fireBall, transform.position, Quaternion.identity);
            list.Add(fb);
            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator SpawnBlueSlime(){
        GameObject bs = Instantiate(blueSlime, transform.position - 2 * boss.Direction * Vector3.right, Quaternion.identity);
        list.Add(bs);
        yield return new WaitForSeconds(0.5f);
    }
}
