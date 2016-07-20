using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {

	private Animator anim;
	private string unarmedCombo1;
	private string unarmedCombo2;
	private string unarmedCombo3;

	private string stagger;
	private string knockback;
	private string pickup;
	private string grappleAttempt;
	private string throwing;

	private PlayerMovement player;
	private float velocityMagnitude;


	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		player = GetComponent<PlayerMovement> ();

		unarmedCombo1 = "unarmed1";
		unarmedCombo2 = "unarmed2";
	}
	
	// Update is called once per frame
	void Update () {
		setIdleRun ("velocityMagnitude");
	}

	private float GetVelocityMagnitude() {
		return player.GetMoveVector ().magnitude;
	}

	// Tarviiko gettereitä ollenkaan..?? \_o.o_/
	public void ResetComboAnimations() {
		SetCombo1 (false);
		SetCombo2 (false);
		SetCombo3 (false);
	}

	public void setIdleRun(string animClip) {
		anim.SetFloat (animClip, GetVelocityMagnitude ());
	}

	public void SetCombo1(bool b) {
		anim.SetBool (unarmedCombo1, b);
	}

	public void SetCombo2(bool b) {
		anim.SetBool (unarmedCombo2, b);
	}

	public void SetCombo3(bool b) {
		anim.SetBool (unarmedCombo3, b);
	}

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
