using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	public static bool isFirst = true;
	public Button start;
	public Button assassin;
	public Button tanker;
	public GameObject credit;
	public void ChooseAssassin(){
		GameManager.ChooseCharacter(true);
		SceneManager.LoadScene("In game scene 1");
	}
	public void ChooseTanker(){
		GameManager.ChooseCharacter(false);
		SceneManager.LoadScene("In game scene 1");
	}
	public void StartGame () {
		assassin.gameObject.SetActive(true);
		tanker.gameObject.SetActive(true);
		start.gameObject.SetActive(false);
	}
	public void HideCredit(){
		credit.SetActive(false);
		
	}
	void Start(){
		if (isFirst){
			HideCredit();
		}
	}
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
