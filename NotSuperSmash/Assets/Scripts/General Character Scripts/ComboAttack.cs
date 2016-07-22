using UnityEngine;
using System.Collections;

public class ComboAttack {

	private GameObject hitboxUsedInAttack;
	private PlayerAnimationHandler animHandler;

	public enum AttackState {startPhase, damagePhase, comboPhase, resetComboPhase};
	public AttackState attackPhase;
	public int comboNumber;

	private bool action1Input;
	private bool action2Input;
	private bool isComboFlagged;
	private bool isActive;
	public bool comboFlagged;

	private string attackType;

	public ComboAttack (PlayerMovement player, GameObject hurtBox, string attackType, int comboNumber) {
		animHandler = player.gameObject.GetComponent<PlayerAnimationHandler> ();

		attackPhase = AttackState.startPhase;
		hitboxUsedInAttack = hurtBox;
		this.comboNumber = comboNumber;
		this.attackType = attackType;
	}

	public void UpdateAttackStates () {

		if (attackPhase == AttackState.startPhase && action1Input) {
			action1Input = false;
			MonoBehaviour.print ("start");
			animHandler.SetAnimationTrigger (attackType, comboNumber);
		} else if (attackPhase == AttackState.startPhase && action2Input) {
			action2Input = false;
			animHandler.SetAnimationTrigger (attackType, comboNumber);
		} else if (attackPhase == AttackState.damagePhase) {
			hitboxUsedInAttack.SetActive (true);
		} else if (attackPhase == AttackState.comboPhase) {
			hitboxUsedInAttack.SetActive (false);
			//ComboFlagged ();
		} else if (attackPhase == AttackState.resetComboPhase) {
			ProcessAndResetCombo ();
		}
	}
		
	public void SetAttackPhase(string toAttackPhase) {
		if (toAttackPhase.Equals ("damagePhase")) {
			this.attackPhase = AttackState.damagePhase;
		} else if (toAttackPhase.Equals ("comboPhase")) {
			this.attackPhase = AttackState.comboPhase;
		} else if (toAttackPhase.Equals ("resetComboPhase")) {
			this.attackPhase = AttackState.resetComboPhase;
		}
	}

	void ProcessAndResetCombo() {
		hitboxUsedInAttack.SetActive (false);
		attackPhase = AttackState.startPhase;
		isComboFlagged = false;
		isActive = false;
	}

	public void ActivateCombo(GameObject hurtBox, string attackType) {
		hitboxUsedInAttack = hurtBox;
		this.attackType = attackType;
		isActive = true;
	}
		
	public bool GetIsComboFlagged() {
		return isComboFlagged;
	}

	public bool GetIsActive() {
		return isActive;
	}

	public void SetIsActive(bool b) {
		isActive = b;
	}
}
