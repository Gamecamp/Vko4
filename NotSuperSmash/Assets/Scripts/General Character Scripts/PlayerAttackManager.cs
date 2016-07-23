using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {

	private GameObject hitboxUsedInAttack;
	private PlayerMovement player;

	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;

	private ComboAttack[] combos;

	private int currentCombo;
	private int maxCombo;

	private bool action1Input;
	private bool action2Input;

	private string activeWeapon;
	private string attackType;

	const string Unarmed = "unarmed";
	const string BaseballBat = "baseballBat";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);

		hitboxUsedInAttack = unarmedLightHitbox;
		activeWeapon = Unarmed;
		currentCombo = 1;
		maxCombo = 3;

		InitializeCombos ();
	}

	// Update is called once per frame
	void Update () {
		UpdateAttackInput ();
		UpdateComboStates ();
		UpdateComboCounter ();
		PrintActives ();
	}

	void InitializeCombos() {
		combos = new ComboAttack[3];

		for (int i = 0; i < combos.Length; i++) {
			combos[i] = new ComboAttack (player, hitboxUsedInAttack, attackType, i + 1, maxCombo);
		}
	}
		
	void UpdateAttackInput () {
		if (!isComboActive ()) {
			if (player.GetIsAction1Input () && player.GetCanInputActions ()) {
				action1Input = true;
				player.SetIsLightAttacking (true);
				DetermineAttackProperties ();
				ActivateComboAttack ();
			} else if (player.GetIsAction2Input () && player.GetCanInputActions ()) {
				action2Input = true;
				player.SetIsHeavyAttacking (true);
				DetermineAttackProperties ();
				ActivateComboAttack ();
			}
		}
	}

	bool isComboActive() {
		bool isComboActive = false;

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].GetIsActive ()) {
				isComboActive = true;
			}
		}
		return isComboActive;
	}

	void UpdateComboStates() {

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].GetIsActive ()) {
				combos [i].UpdateAttackStates ();
			}
		}
	}

	void UpdateComboCounter() {
		print ("manager, currentCombo = " + currentCombo);
		for (int i = 0; i < combos.Length; i++) {
			ContinueCombo (combos [i]);
			ResetCombo (combos[i]);
		}
	}

	void ContinueCombo(ComboAttack combo) {
		if (combo.GetIsActive () && combo.GetIsComboFlagged ()) {
			currentCombo++;
			combo.currentCombo = this.currentCombo;
			ActivateComboAttack ();
			combo.SetIsComboFlagged (false);
		}
	}

	void ResetCombo(ComboAttack combo) {
		if (combo.GetResetCombo ()) {
			player.SetIsLightAttacking (false);
			player.SetIsHeavyAttacking (false);
			action1Input = false;
			action2Input = false;
			currentCombo = 1;

			combo.SetResetCombo (false);
			print ("manager reset!!");
		}
	}

	void ActivateComboAttack() {

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].comboNumber == currentCombo) {
				combos [i].ActivateCombo (hitboxUsedInAttack, attackType, action1Input, action2Input, currentCombo);
			}
		}
	}

/// <summary>
/// ****//////// PRINTTITIÄIATÄIATÄIAT
/// </summary>
	void PrintActives() {
		float huh = 00;

		for (int i = 0; i < combos.Length; i++) {
			
			if (combos [i].GetIsActive ()) {
				huh++;
				print ("Combo number " + combos [i].comboNumber + " is active.");
			}
		}

		print (huh);
		huh = 0;
	}

	void DetermineAttackProperties() {
		switch (activeWeapon) {
		case Unarmed:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = unarmedLightHitbox;
				attackType = "unarmedLight";
				maxCombo = 3;
			} else {
				hitboxUsedInAttack = unarmedHeavyHitbox;
				attackType = "unarmedHeavy";
				maxCombo = 1;
			}
			break;
		case BaseballBat:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = baseballBatLightHitbox;
				attackType = "meleeLight";
				maxCombo = 3;
			} else {
				hitboxUsedInAttack = baseballBatHeavyHitbox;
				attackType = "meleeHeavy";
				maxCombo = 1;
			}
			break;
		}
	}
		
	public void ChangeAttackPhase(string toAttackPhase) {

		for (int i = 0; i < combos.Length; i++) {
			if ( (toAttackPhase.Equals ("damagePhase1") || toAttackPhase.Equals ("comboPhase1") ||
				toAttackPhase.Equals ("resetComboAttackPhase1")) && combos [i].comboNumber == 1) {
				combos [i].SetAttackPhase (toAttackPhase.Substring(0, toAttackPhase.Length - 1));
				print ("event");
			} else if ( (toAttackPhase.Equals ("damagePhase2") || toAttackPhase.Equals ("comboPhase2") || 
				toAttackPhase.Equals ("resetComboAttackPhase2")) && combos[i].comboNumber == 2) {
				combos [i].SetAttackPhase (toAttackPhase.Substring(0, toAttackPhase.Length - 1));
			} else if ( (toAttackPhase.Equals ("damagePhase3") || toAttackPhase.Equals ("comboPhase3") ||
				toAttackPhase.Equals ("resetComboAttackPhase3")) && combos [i].comboNumber == 3) {
				combos [i].SetAttackPhase (toAttackPhase.Substring(0, toAttackPhase.Length - 1));
			}
		}
	}
		
	void ResetAttack() {
		player.SetIsLightAttacking (false);
		player.SetIsHeavyAttacking (false);
		currentCombo = 1;
	}

	void ResetAttackChain() {
		currentCombo = 1;
	}

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}
}