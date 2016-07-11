﻿using UnityEngine;
using System.Collections;

public class PlayerGrapple : MonoBehaviour {

	PlayerMovement player;

	PlayerMovement targetPlayer;

	GameObject grappleBox;

	private float grappleAttemptDuration = 0;
	private float grappleAttemptWindupDuration = 0.15f;
	private float grappleAttemptMaxDuration = 0.3f;

	private float grappleDuration = 0;
	private float grappleMaxDuration = 1f;

	private bool grappleWindupGoing;
	private bool grappleBoxActive;

	private bool grappleIsFinished;
	private bool grappleIsHappening; 

	Vector3 throwingVector;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		grappleBox = GameObject.Find ("GrapplingHitboxP1");
		grappleBox.SetActive(false);
		grappleBoxActive = false;
		grappleIsHappening = false;
		grappleAttemptDuration = 0;
	}

	// Update is called once per frame
	void Update () {
		UpdateGrapplingAttempt ();
		UpdateGrappling ();
	}
	
	void UpdateGrapplingAttempt () {
		
		if (player.GetIsThrowingInput() && player.GetCanInputActions()) {
			grappleWindupGoing = true;
		}

		if (grappleWindupGoing && !grappleBoxActive) {
			player.SetCanMove(false);
			grappleAttemptDuration = grappleAttemptDuration + Time.deltaTime;
		}

		if (grappleAttemptDuration >= grappleAttemptWindupDuration) {

			grappleWindupGoing = false;
			grappleBox.SetActive (true);
			grappleBoxActive = true;
			player.SetCanMove (false);

			grappleAttemptDuration = grappleAttemptDuration + Time.deltaTime;

			if (grappleAttemptDuration >= grappleAttemptMaxDuration) {
				grappleBox.SetActive (false);
				grappleBoxActive = false;
				grappleAttemptDuration = 0;

				if (!grappleIsHappening) {
					player.SetCanMove (true);
				}
			}
		} 

	}

	void UpdateGrappling() {
		if (grappleIsHappening) {

			print ("grappel");

			throwingVector = new Vector3(InputManager.GetXInput (gameObject.name), 0, InputManager.GetZInput (gameObject.name));

			print (Mathf.Abs(throwingVector.x + throwingVector.z));

			if (Mathf.Abs(throwingVector.x) > 0.5f || Mathf.Abs(throwingVector.z) > 0.5f) {
				print ("thrower");

				MyPhysics.ApplyKnockback(targetPlayer, throwingVector, 50);
				ResetGrapple ();
			}

			print (throwingVector);

			PassGrapplingTime ();
			if (grappleIsFinished) {
				ResetGrapple ();
			}
		}
	}

	public void BeginGrappling(PlayerMovement targetPlayer) {
		
		grappleIsHappening = true;
		grappleIsFinished = false;

		this.targetPlayer = targetPlayer;

		SetGrapplers ();
		TurnGrapplers ();
	}

	void SetGrapplers() {

		targetPlayer.SetIsGrappled (true);
		targetPlayer.SetCanInputActions (false);

		player.SetIsGrappling (true);
		player.SetCanMove (false);

	}

	void TurnGrapplers() {

		player.LookTowards (targetPlayer.transform.position);

		targetPlayer.LookTowards(player.transform.position);
	}

	void PassGrapplingTime() {
		grappleDuration = grappleDuration + Time.deltaTime;

		if (grappleDuration >= grappleMaxDuration) {
			grappleIsFinished = true;

		}
	}

	void ResetGrapple() {
		targetPlayer.SetIsGrappled (false);
		targetPlayer.SetCanInputActions (true);

		player.SetIsGrappling (false);
		player.SetCanMove (true);

		grappleIsFinished = false;
		grappleIsHappening = false;

		grappleDuration = 0;
	}

	public bool GetGrappleIsHappening() {
		return grappleIsHappening;
	}
		
}
