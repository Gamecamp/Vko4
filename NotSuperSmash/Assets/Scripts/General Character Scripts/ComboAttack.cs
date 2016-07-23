using UnityEngine;
using System.Collections;

public class ComboAttack {

	private PlayerMovement player;
	private GameObject hitboxUsedInAttack;
	private PlayerAnimationHandler animHandler;

	public enum AttackState {startPhase, damagePhase, comboPhase, resetComboAttackPhase};
	public AttackState attackPhase;
	public int comboNumber;
	public int currentCombo;
	private int maxCombo;

	private bool action1Input;
	private bool action2Input;
	private bool isComboFlagged;
	private bool isActive;
	private bool resetCombo;

	private string attackType;

	public ComboAttack (PlayerMovement player, GameObject hurtBox, string attackType, int comboNumber, int maxCombo) {
		animHandler = player.gameObject.GetComponent<PlayerAnimationHandler> ();
		this.player = player;

		attackPhase = AttackState.startPhase;
		hitboxUsedInAttack = hurtBox;
		this.comboNumber = comboNumber;
		this.maxCombo = maxCombo;
		this.attackType = attackType;
		isComboFlagged = false;
		resetCombo = false;
	}

	public void UpdateAttackStates () {

		if (attackPhase == AttackState.startPhase && action1Input) {
			action1Input = false;
			animHandler.SetAnimationTrigger (attackType, comboNumber);
		} else if (attackPhase == AttackState.startPhase && action2Input) {
			action2Input = false;
			animHandler.SetAnimationTrigger (attackType, comboNumber);
		} else if (attackPhase == AttackState.damagePhase) {
			hitboxUsedInAttack.SetActive (true);
		} else if (attackPhase == AttackState.comboPhase) {
			hitboxUsedInAttack.SetActive (false);

			if (comboNumber < maxCombo) {
				IsComboBeingActivated ();
			}

		} else if (attackPhase == AttackState.resetComboAttackPhase) {
			ProcessAndResetComboAttack ();
			MonoBehaviour.print ("endPhase");
		}
	}
		
	public void SetAttackPhase(string toAttackPhase) {
		if (toAttackPhase.Equals ("damagePhase")) {
			this.attackPhase = AttackState.damagePhase;
		} else if (toAttackPhase.Equals ("comboPhase")) {
			this.attackPhase = AttackState.comboPhase;
		} else if (toAttackPhase.Equals ("resetComboAttackPhase")) {
			this.attackPhase = AttackState.resetComboAttackPhase;
		}
	}

	void IsComboBeingActivated() {
		if (attackType.Contains ("Light") && player.GetIsAction1Input()) {
			isComboFlagged = true;
			MonoBehaviour.print ("combo");
		}

		if (attackType.Contains ("Heavy") && player.GetIsAction2Input()) {
			isComboFlagged = true;
		}
	}

	void ProcessAndResetComboAttack() {
		hitboxUsedInAttack.SetActive (false);
		isComboFlagged = false;
		isActive = false;
		attackType = "";
		attackPhase = AttackState.startPhase;

		if (IsEndCombo ()) {
			resetCombo = true;
		}
		currentCombo = 1;
	}

	public void ActivateCombo(GameObject hurtBox, string attackType, bool action1, bool action2, int curCombo) {
		hitboxUsedInAttack = hurtBox;
		this.attackType = attackType;
		action1Input = action1;
		action2Input = action2;
		isActive = true;
		this.currentCombo = curCombo;
	}

	private bool IsEndCombo() {
		bool end = false;
		MonoBehaviour.print (comboNumber + " combo's currentCombo = " + currentCombo);
		if (comboNumber >= currentCombo) {
			end = true;
		}
		return end;
	}

	public bool GetResetCombo() {
		return resetCombo;
	}

	public void SetResetCombo(bool b) {
		resetCombo = b;
	}
		
	public bool GetIsComboFlagged() {
		return isComboFlagged;
	}

	public void SetIsComboFlagged(bool b) {
		isComboFlagged = b;
	}

	public bool GetIsActive() {
		return isActive;
	}

	public void SetIsActive(bool b) {
		isActive = b;
	}
}
