using UnityEngine;
using System.Collections;

public class PlayerGrapple : MonoBehaviour {

	PlayerMovement player;

	PlayerMovement targetPlayer;

	GameObject grappleBox;

	private float grappleAttemptDuration = 0;
	private float grappleAttemptMaxDuration = 0.3f;

	private float grappleDuration = 0;
	private float grappleMaxDuration = 0.5f;

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
	}

	// Update is called once per frame
	void Update () {
		UpdateGrapplingAttempt ();
		UpdateGrappling ();
	}

	void UpdateGrapplingAttempt () {
		if (player.GetIsThrowingInput() && player.GetCanInputActions()) {
			grappleBox.SetActive (true);
			grappleBoxActive = true;
		} 

		if (grappleBoxActive) {

			player.SetCanMove (false);

			grappleAttemptDuration = grappleAttemptDuration + Time.deltaTime;

			if (grappleAttemptDuration >= grappleAttemptMaxDuration) {
				grappleBox.SetActive (false);
				grappleBoxActive = false;
				grappleAttemptDuration = 0;
			}
		} else {
			player.SetCanMove (true);
		}

	}

	void UpdateGrappling() {
		if (grappleIsHappening) {

			print ("grappel");

			throwingVector = new Vector3(InputManager.GetXInput (gameObject.name), 0, InputManager.GetYInput (gameObject.name));

			print (Mathf.Abs(throwingVector.x + throwingVector.z));

			if (Mathf.Abs (throwingVector.x + throwingVector.z) > 1) {
				print ("thrower");

				MyPhysics.ApplyKnockback(targetPlayer, player.transform.position, throwingVector, 200);
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
