using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapManager : MonoBehaviour {

	List<Monster> monsterList;
	public GameObject weaponPrize;
	public string nextScene;
	int initialCount;
	bool halfPrized = false;

	void Start () {
		var monsters = FindObjectsOfType<Monster>();
		monsterList = monsters.ToList();
		initialCount = monsterList.Count;
		monsterList.All(m => {
			m.ConnectWithMap(this);
			return true;
		});
	}
	public void MonsterRemoved(Monster monster){
		if (monsterList.Contains(monster)){
			monsterList.Remove(monster);
			Debug.Log("monster removed");
			if (!halfPrized && monsterList.Count <= initialCount/2){
				halfPrized = true;
				GameManager.Instance.NextPirze();
			} else if (monsterList.Count < 1){
				GameManager.Instance.NextPirze();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player>() != null){
			GameManager.Instance.MoveScene(nextScene);
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
