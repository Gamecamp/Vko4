using UnityEngine;
using System.Collections;

public class PlayerGrapple : MonoBehaviour {

	PlayerMovement player;
	PlayerMovement targetPlayer;
	GameObject targetPlayerObject;

	public GameObject playerLocation;
	GameObject targetPlayerLocation;

	public GameObject grappleBox;

	private float grappleAttemptDuration = 0;
	private float grappleAttemptWindupDuration = 0.2f;
	private float grappleAttemptMaxDuration = 0.7f;

	private float grappleDuration = 0;
	private float grappleMaxDuration = 5;

	private bool grappleIsFinished;
	private bool grappleIsHappening;
	private bool grappleAttemptInProgress;

	private bool grappleWindupGoing;

	Vector3 throwingVector;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		grappleBox.SetActive(false);
		grappleIsHappening = false;
		grappleIsFinished = false;
		grappleAttemptInProgress = false;
		grappleWindupGoing = false;
		grappleAttemptDuration = 0;
	}

	// Update is called once per frame
	void Update () {
		UpdateGrapplingAttempt ();
		UpdateGrappling ();
	}
	
	void UpdateGrapplingAttempt () {
		
		if (player.GetIsThrowingInput() && player.GetCanInputActions() && player.GetIsAbleToEquip()) {
			player.SetIsGrappling (true);
			grappleWindupGoing = true;
			grappleAttemptInProgress = true;
		}

		if (player.GetIsGrappling ()) {


			grappleAttemptDuration = grappleAttemptDuration + Time.deltaTime;

			if (grappleWindupGoing && grappleAttemptDuration >= grappleAttemptWindupDuration) {
				grappleBox.SetActive (true);
				grappleWindupGoing = false;
			}

			if (grappleAttemptDuration >= grappleAttemptMaxDuration) {
				grappleBox.SetActive (false);
				grappleAttemptDuration = 0;
				ResetGrappleAttempt ();
			}
		} else if (grappleAttemptInProgress) {
			ResetGrappleAttempt ();
		}

	}

	void ResetGrappleAttempt() {
		grappleIsFinished = false;
		grappleIsHappening = false;
		grappleAttemptInProgress = false;

		grappleWindupGoing = false;
		grappleDuration = 0;
		grappleAttemptDuration = 0;

		player.SetIsGrappling (false);
	}

	void UpdateGrappling() {
		if (grappleIsHappening) {
			throwingVector = new Vector3(InputManager.GetXInput (gameObject.name), 0, InputManager.GetZInput (gameObject.name));

			if (Mathf.Abs(throwingVector.x) > 0.5f || Mathf.Abs(throwingVector.z) > 0.5f) {
				MyPhysics.ApplyKnockback(targetPlayer, throwingVector, 50);
				ResetGrapple ();
			}

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
		targetPlayerLocation = GameObject.Find ("PlayerPosition" + targetPlayer.name);

		SetGrapplers ();
		TurnGrapplers ();
	}

	void SetGrapplers() {
		targetPlayer.SetIsGrappled (true);
		player.SetIsGrappling (true);
	}

	void TurnGrapplers() {
		player.LookTowards (targetPlayerLocation.transform.position);
		targetPlayer.LookTowards(playerLocation.transform.position);
	}

	void PassGrapplingTime() {
		grappleDuration = grappleDuration + Time.deltaTime;
		if (grappleDuration >= grappleMaxDuration) {
			grappleIsFinished = true;
		}
	}

	void ResetGrapple() {
		
		targetPlayer.SetIsGrappled (false);
		player.SetIsGrappling (false);

		grappleIsFinished = false;
		grappleIsHappening = false;
		grappleAttemptInProgress = false;

		grappleWindupGoing = false;

		grappleDuration = 0;
	}

	public bool GetGrappleIsHappening() {
		return grappleIsHappening;
	}
}
