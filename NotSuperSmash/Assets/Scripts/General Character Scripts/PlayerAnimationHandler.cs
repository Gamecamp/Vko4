using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {

	private Animator anim;
	private string combo1;
	private string comboStop1;
	private string combo2;
	private string comboStop2;
	private string combo3;
	private string comboStop3;

	private string running;
	private string stagger;
	private string knockback;
	private string pickup;
	private string grappleAttempt;
	private string throwing;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Tarviiko gettereitä ollenkaan..?? \_o.o_/

	public void SetCombo1(bool b) {
		anim.SetBool (combo1, b);
	}

	public void SetCombo2(bool b) {
		anim.SetBool (combo2, b);
	}

	public void SetCombo3(bool b) {
		anim.SetBool (combo3, b);
	}

	public void SetComboStop1(bool b) {
		anim.SetBool (comboStop1, b);
	}

	public void SetComboStop2(bool b) {
		anim.SetBool (comboStop2, b);
	}

	public void SetComboStop3(bool b) {
		anim.SetBool (comboStop3, b);
	}

	public void SetRunning(bool b) {
		anim.SetBool (running, b);
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
