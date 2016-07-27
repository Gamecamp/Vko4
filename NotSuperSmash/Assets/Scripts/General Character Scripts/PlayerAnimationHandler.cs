using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {

	private PlayerMovement player;
	private Animator anim;

	private string unarmedLightCombo;
	private string unarmedHeavyCombo;
	private string meleeLightCombo;
	private string meleeHeavyCombo;
	private string meleeSpearLightCombo;
	private string meleeSpearHeavyCombo;

	private string stagger;
	private string knockback;
	private string pickup;
	private string grappleAttempt;
	private string throwing;

	private float velocityMagnitude;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		player = GetComponent<PlayerMovement> ();

		unarmedLightCombo = "unarmedLight";
		unarmedHeavyCombo = "unarmedHeavy";

		meleeLightCombo = "meleeLight";
		meleeHeavyCombo = "meleeHeavy";

		meleeSpearLightCombo = "meleeSpearLight";
		meleeSpearHeavyCombo = "meleeSpearHeavy";
	}
	
	// Update is called once per frame
	void Update () {
		setIdleRun ("velocityMagnitude");
	}

	private float GetVelocityMagnitude() {
		return player.GetMoveVector ().magnitude;
	}

	public void setIdleRun(string animClip) {
		anim.SetFloat (animClip, GetVelocityMagnitude ());
	}

	public void SetAnimationTrigger(string attackType) {
		anim.SetTrigger (attackType);
	}

	// *** OTHER *** //
	public void SetStagger(bool b) {
		anim.SetBool (stagger, b);
	}

	public void SetPickup(bool b) {
		anim.SetBool (pickup, b);
	}

	public void SetGrappleAttempt(bool b) {
		anim.SetBool (grappleAttempt, b);
	}

	public void SetThrowing(bool b) {
		anim.SetBool (throwing, b);
	}
}
