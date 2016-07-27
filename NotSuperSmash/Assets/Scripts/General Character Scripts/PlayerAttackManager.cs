using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {
	
	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;

	private GameObject hitboxUsedInAttack;
	private PlayerMovement player;
	private PlayerAnimationHandler animHandler;

	private bool action1Input;
	private bool action2Input;
	private bool attackInProgress;
	private bool canCombo;

	private string activeWeapon;
	private string attackType;

	const string Unarmed = "unarmed";
	const string BaseballBat = "baseballBat";

	void Start () {
		player = GetComponent<PlayerMovement> ();
		animHandler = GetComponent<PlayerAnimationHandler> ();

		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);

		hitboxUsedInAttack = unarmedLightHitbox;
		activeWeapon = Unarmed;

		attackInProgress = false;
		canCombo = false;
	}
		
	void Update () {

		if (GetAttackInput () && player.GetCanInputActions ()) {
			if (attackInProgress == false) {
				StartCombo ();
			} else if (attackInProgress && canCombo) {
				animHandler.SetAnimationTrigger (attackType);
			}
		}

		if (!player.GetIsLightAttacking () && !player.GetIsHeavyAttacking ()) {
			DeactivateHitbox ();
			EndCombo ();
		}
	}

	private bool GetAttackInput() {
		if (player.GetIsAction1Input ()) {
			player.SetIsLightAttacking (true);
			return true;
		} else if (player.GetIsAction2Input ()) {
			player.SetIsHeavyAttacking (true);
			return true;
		} else {
			return false;
		}
	}
	private void StartCombo () {
		DetermineAttackProperties ();
		animHandler.SetAnimationTrigger (attackType);
		attackInProgress = true;
	}

	public void ActivateHitbox() {
		hitboxUsedInAttack.SetActive (true);
		canCombo = true;
	}

	public void DeactivateHitbox() {
		hitboxUsedInAttack.SetActive (false);
		canCombo = false;
	}

	public void EndCombo() {
		player.SetIsLightAttacking (false);
		player.SetIsLightAttacking (false);
		attackInProgress = false;
	}

	void DetermineAttackProperties() {
		switch (activeWeapon) {
		case Unarmed:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = unarmedLightHitbox;
				attackType = "unarmedLight";
			} else {
				hitboxUsedInAttack = unarmedHeavyHitbox;
				attackType = "unarmedHeavy";
			}
			break;
		case BaseballBat:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = baseballBatLightHitbox;
				attackType = "meleeLight";
			} else {
				hitboxUsedInAttack = baseballBatHeavyHitbox;
				attackType = "meleeHeavy";
			}
			break;
		}
	}

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}
}