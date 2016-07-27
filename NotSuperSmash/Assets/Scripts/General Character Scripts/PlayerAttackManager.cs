using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {
	
	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;
	public GameObject katanaLightHitbox;
	public GameObject katanaHeavyHitbox;
	public GameObject rangedWeaponPseudoHitbox;

	private GameObject hitboxUsedInAttack;
	private PlayerMovement player;
	private PlayerAnimationHandler animHandler;

	private bool attackInProgress;
	private bool canCombo;

	private string activeWeapon;
	private string attackType;

	const string unarmed = "unarmed";
	const string baseballBat = "baseballBat";
	const string pistol = "pistol";
	const string shotgun = "shotgun";
	const string katana = "katana";
	const string sawedOff = "sawedOff";

	void Start () {
		player = GetComponent<PlayerMovement> ();
		animHandler = GetComponent<PlayerAnimationHandler> ();

		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);
		katanaLightHitbox.SetActive (false);
		katanaHeavyHitbox.SetActive (false);
		rangedWeaponPseudoHitbox.SetActive (false);
		attackInProgress = false;

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

	/*
			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
				if (activeWeapon == pistol) {
					GetComponent<Bullet> ().Shoot (0.5f);
				} else if (activeWeapon == shotgun) {
					GetComponent<Bullet> ().Shoot (1.5f);
				} else if (activeWeapon == sawedOff) {
					GetComponent<Bullet> ().Shoot (1.5f);
				} else {
					hitboxUsedInAttack.SetActive (true);
				}
				attackPhaseHelper++;
			} 
	*/

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
		case katana:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = katanaLightHitbox;

				beforeHurtAnimationLength = 0.2f;
				hurtfulAnimationLength = 0.2f;
				recoveryTime = 0.4f;

				maxChain = 3;
			} else {
				hitboxUsedInAttack = katanaHeavyHitbox;

				beforeHurtAnimationLength = 0.4f;
				hurtfulAnimationLength = 0.2f;
				recoveryTime = 0.8f;

				maxChain = 1;
			}
			break;
		case pistol:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;

			maxChain = 8;
			break;
		case shotgun:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;

			maxChain = 5;
			break;
		case sawedOff:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;

			maxChain = 5;
			break;
		}
	}

	/* KYSY RELOADISTA!!!!!!
	void ResetAttackChain() {
		numberInAttackChain = 1;
		//if (activeWeapon == pistol) {
		//	GetComponent<Bullet> ().Reload (5f);
		//}
	}
	*/

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}
}