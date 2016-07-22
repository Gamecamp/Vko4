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
			combos[i] = new ComboAttack (player, hitboxUsedInAttack, attackType, i + 1);
		}
	}
		
	void UpdateComboStates() {

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].GetIsActive ()) {
				combos [i].UpdateAttackStates ();
			}
		}
	}

	void PrintActives() {
		float huh = 00;

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].GetIsActive ()) {
				huh++;
			}
		}

				print (huh);
				huh = 0;
	}

	void UpdateComboCounter() {
		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].GetIsActive ()) {
				if (combos [i].comboNumber > currentCombo) {
					currentCombo = combos [i].comboNumber;
				}else if (combos [i].GetIsComboFlagged ()) {
					currentCombo = combos [i].comboNumber;
					combos [i].comboFlagged = false;
				}
			}

		}
	}

	void ActivateCombo() {

		for (int i = 0; i < combos.Length; i++) {
			if (combos [i].comboNumber == currentCombo) {
				combos [i].ActivateCombo (hitboxUsedInAttack, attackType);
			}
		}
	}

	void UpdateAttackInput () {

		if (player.GetIsAction1Input () && player.GetCanInputActions ()) {
			action1Input = true;
			player.SetIsLightAttacking (true);
			DetermineAttackProperties ();
			ActivateCombo ();
			print ("dd");
		} else if (player.GetIsAction2Input () && player.GetCanInputActions ()) {
			action2Input = true;
			player.SetIsHeavyAttacking (true);
			DetermineAttackProperties ();
			ActivateCombo ();
		}
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
				toAttackPhase.Equals ("resetComboPhase1")) && combos [i].comboNumber == 1) {
				combos [i].SetAttackPhase (toAttackPhase.Substring(0, toAttackPhase.Length - 1));
				print("jea");
			} else if ( (toAttackPhase.Equals ("damagePhase2") || toAttackPhase.Equals ("comboPhase2") || 
				toAttackPhase.Equals ("resetComboPhase2")) && combos[i].comboNumber == 2) {
				combos [i].SetAttackPhase (toAttackPhase.Substring(0, toAttackPhase.Length - 1));
			} else if ( (toAttackPhase.Equals ("damagePhase3") || toAttackPhase.Equals ("comboPhase3") ||
				toAttackPhase.Equals ("resetComboPhase3")) && combos [i].comboNumber == 3) {
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