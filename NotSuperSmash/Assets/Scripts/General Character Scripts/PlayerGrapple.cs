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
	private float grappleInputThreshold = 0.3f;
	private float grappleMaxDuration = 1;

	private bool grappleIsFinished;
	private bool grappleIsHappening;
	private bool grappleAttemptInProgress;

	private bool attemptReset;

	private bool grappleWindupGoing;

	Vector3 throwingVector;

	Vector2 joystickInput;

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
			grappleAttemptInProgress = true;
			player.SetIsAttemptingGrapple (true);
			grappleWindupGoing = true;
			attemptReset = false;
		}

		if (player.GetIsAttemptingGrapple()) {


			grappleAttemptDuration = grappleAttemptDuration + Time.deltaTime;

			if (grappleWindupGoing && grappleAttemptDuration >= grappleAttemptWindupDuration) {
				grappleBox.SetActive (true);
				grappleWindupGoing = false;
			}

			if (grappleAttemptDuration >= grappleAttemptMaxDuration) {
				grappleAttemptDuration = 0;
				ResetGrappleAttempt ();
			}
		} else if (grappleAttemptInProgress) {
			ResetGrappleAttempt ();
		}

	}

	void ResetGrappleAttempt() {
		grappleIsFinished = false;
		grappleAttemptInProgress = false;
		attemptReset = true;
		grappleBox.SetActive (false);

		grappleWindupGoing = false;
		grappleDuration = 0;
		grappleAttemptDuration = 0;

		player.SetIsAttemptingGrapple (false);
	}

	void UpdateGrappling() {
		if (grappleIsHappening) {
			if (!attemptReset) {
				ResetGrappleAttempt ();
				player.SetIsGrappling (true);
				player.SetIsAttemptingGrapple (false);
			}

			if (grappleDuration >= grappleInputThreshold) {
				joystickInput = InputManager.GetJoystickInput (gameObject.name);
				throwingVector = new Vector3 (joystickInput.x, 0, joystickInput.y);

				if (Mathf.Abs (throwingVector.x) > 0.5f || Mathf.Abs (throwingVector.z) > 0.5f) {
					MyPhysics.ApplyKnockback (targetPlayer, throwingVector, 50);
					ResetGrapple ();
				}
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
