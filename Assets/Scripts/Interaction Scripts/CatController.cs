﻿using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	public AudioClip[] idle;
	public AudioClip attack;
	private bool isBusy;

	// Use this for initialization
	void Start () {
		GameController.Register ("Cat", transform);
		InvokeRepeating ("IdleSound", 0, 4);
		isBusy = false;
	}

	public void playAttack(){
		audio.clip = attack;
		audio.Play ();
	}
	
	void IdleSound(){

		if (!isBusy) {
			audio.clip = idle [Random.Range (0, idle.GetLength (0))];
			audio.Play ();	
		}
		
	}
	
	public void setBusy(bool busy){
		isBusy = busy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
