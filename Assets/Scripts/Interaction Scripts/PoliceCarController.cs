﻿using UnityEngine;
using System.Collections;

public class PoliceCarController : MonoBehaviour, InteractableObject {

	private PlayerController player;
	private bool facingRight;
	public AudioClip catDeath;

	// Use this for initialization
	void Start () {
		GameController.Register ("PoliceCar", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool onPush (bool right) {
		facingRight = right;
		//Start Push Car event
		StartCoroutine ("PushCarEvent");
		return facingRight;

	}

	public bool onLift () {
		return false;
	}

	IEnumerator PushCarEvent () {

		if (facingRight) {

						//Set Policeman to untagged
						//Set Car to untagged
						GameController.Get ("PoliceCar").tag = "Untagged";
						GameController.Get ("Policeman").tag = "Untagged";

						//Set character to uncontrollable
						PlayerController player = GameController.Get ("Player").GetComponent<PlayerController> ();
						player.canControl = false;

						//Move Car To Tree
						while (GameController.Get ("PoliceCar").position.x < 1) {
								GameController.Get ("PoliceCar").transform.Translate (Time.deltaTime, 0, 0);
								yield return null;
						}
						while (GameController.Get ("PoliceCar").position.x > 0.9) {
								GameController.Get ("PoliceCar").transform.Translate (-Time.deltaTime, 0, 0);
								yield return null;
						}

						//Change cat to fallen/dead
						Transform cat = GameController.Get ("Cat");
						cat.GetComponent<AudioSource>().clip = catDeath;
						cat.GetComponent<AudioSource>().Play ();
						cat.GetComponentInChildren<Animator>().SetBool("isScratching", true);
						GameController.Get ("Tree").transform.GetComponentInChildren<Animator>().SetBool("Shaking", true);
						while(cat.transform.position.y > -3.7){
							cat.transform.Translate(new Vector3(0,-5*Time.deltaTime,0), Space.World);
							cat.transform.Rotate(Vector3.forward * Time.deltaTime* 120); //it's good enough
							yield return null;
							}
						cat.transform.rotation = Quaternion.identity;
						GameController.Get ("Tree").GetComponentInChildren<Animator>().SetBool("Shaking", false);
						cat.GetComponentInChildren<Animator>().SetBool("isScratching", false);
						cat.GetComponentInChildren<Animator>().SetTrigger("Dead");
						cat.audio.mute = true;
						
						//Change tree to fallen
						GameController.Get ("Tree").GetComponentInChildren<Animator>().SetTrigger("Burn");
			
						//Change tree to burning
				}
		else
		yield return null;

	}
}
