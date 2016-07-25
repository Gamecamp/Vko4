using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {

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

	private PlayerMovement player;
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

	// Tarviiko gettereitä ollenkaan..?? \_o.o_/
	public void ResetComboTriggers() {
		SetAnimationTrigger (unarmedLightCombo, 1);
		SetAnimationTrigger (unarmedLightCombo, 2);
		SetAnimationTrigger (unarmedLightCombo, 3);
		SetAnimationTrigger (unarmedHeavyCombo, 1);

		SetAnimationTrigger (meleeLightCombo, 1);
		SetAnimationTrigger (meleeLightCombo, 2);
		SetAnimationTrigger (meleeLightCombo, 3);
		SetAnimationTrigger (meleeHeavyCombo, 1);

		SetAnimationTrigger (meleeSpearLightCombo, 1);
		SetAnimationTrigger (meleeSpearLightCombo, 2);
		SetAnimationTrigger (meleeSpearLightCombo, 3);
		SetAnimationTrigger (meleeSpearHeavyCombo, 1);
	}

	public void setIdleRun(string animClip) {
		anim.SetFloat (animClip, GetVelocityMagnitude ());
	}

	public void SetAnimationTrigger(string attackType) {
		anim.SetTrigger (attackType);
		//print (attackType + comboNumber);
	}

	public void SetAnimationTrigger(string attackType, int comboNumber) {
		anim.SetTrigger (attackType + comboNumber);
		//print (attackType + comboNumber);
	}

//	// *** UNARMED *** //
//	public void SetUnarmedLightCombo(int comboNumber, bool b) {
//		anim.SetBool (unarmedLightCombo + comboNumber, b);
//	}
//
//	public void SetUnarmedHeavyCombo(int comboNumber, bool b) {
//		anim.SetBool (unarmedHeavyCombo + comboNumber, b);
//	}
//
//	// *** MELEE *** //
//	public void SetMeleeLightCombo(int comboNumber, bool b) {
//		anim.SetBool (meleeLightCombo + comboNumber, b);
//	}
//
//	public void SetMeleeHeavyCombo(int comboNumber, bool b) {
//		anim.SetBool (meleeHeavyCombo + comboNumber, b);
//	}
//
//	// *** SPEAR *** //
//	public void SetMeleeSpearLightCombo(int comboNumber, bool b) {
//		anim.SetBool (meleeSpearLightCombo + comboNumber, b);
//	}
//
//	public void SetMeleeSpearHeavyCombo(int comboNumber, bool b) {
//		anim.SetBool (meleeSpearHeavyCombo + comboNumber, b);
//	}

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
