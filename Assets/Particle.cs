using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

	Animator anim;
	float time;
	float duration;
	void Awake(){
		anim = GetComponent<Animator>();
	}
	void OnEnable () {
		anim.SetTrigger("do");
		duration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		time = 0;
	}
	
	void Update () {
		if (isActiveAndEnabled){
			time += Time.deltaTime;
			if (time >= duration){
				gameObject.SetActive(false);
				GameManager.Instance.EnqueueParticle(gameObject);
			}
		}	
	}
}
