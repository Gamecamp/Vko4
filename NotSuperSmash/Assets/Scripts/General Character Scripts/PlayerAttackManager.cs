using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {
	
	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;
	public GameObject katanaLightHitbox;
	public GameObject katanaHeavyHitbox;
	public GameObject spearLightHitbox;
	public GameObject spearHeavyHitbox;
	public GameObject rangedWeaponPseudoHitbox;

	private GameObject hitboxUsedInAttack;
	private PlayerMovement player;
	private PlayerAnimationHandler animHandler;
	private Bullet _bullet;

	private bool attackInProgress;
	private bool canCombo;

	private string activeWeapon;
	private string attackType;
	private float rateOfFire;


	const string Spear = "spear";
	const string Unarmed = "unarmed";
	const string BaseballBat = "baseballBat";
	const string Pistol = "pistol";
	const string Shotgun = "shotgun";
	const string Katana = "katana";
	const string SawedOff = "sawedOff";

	void Start () {
		player = GetComponent<PlayerMovement> ();
		animHandler = GetComponent<PlayerAnimationHandler> ();
		_bullet = GetComponent<Bullet> ();

		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);
		katanaLightHitbox.SetActive (false);
		katanaHeavyHitbox.SetActive (false);
		spearLightHitbox.SetActive (false);
		spearHeavyHitbox.SetActive (false);
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
		attackInProgress = true;

		if (attackType == "ranged") {
			_bullet.Shoot (rateOfFire);
			EndCombo ();
		} else {
			animHandler.SetLayerWeight (1, 0f);
			animHandler.SetAnimationTrigger (attackType);
		}
	}

	public void ActivateHitbox() {
		hitboxUsedInAttack.SetActive (true);
		canCombo = true;
	}

	/*
		if (activeWeapon == pistol) {
			GetComponent<Bullet> ().Shoot (0.5f);
		} else if (activeWeapon == shotgun) {
			GetComponent<Bullet> ().Shoot (1.5f);
		} else if (activeWeapon == sawedOff) {
			GetComponent<Bullet> ().Shoot (1.5f);
		} else {
			hitboxUsedInAttack.SetActive (true);
		}
	*/

	public void DeactivateHitbox() {
		hitboxUsedInAttack.SetActive (false);
		canCombo = false;
	}

	public void EndCombo() {
		player.SetIsLightAttacking (false);
		player.SetIsHeavyAttacking (false);
		attackInProgress = false;
		animHandler.SetLayerWeight (1, 1);
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
		case Katana:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = katanaLightHitbox;
				attackType = "meleeLight";
			} else {
				hitboxUsedInAttack = katanaHeavyHitbox;
				attackType = "meleeHeavy";
			}
			break;
		case Pistol:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;
			attackType = "ranged";
			rateOfFire = 0.5f;
			//maxChain = 8;
			break;
		case Shotgun:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;
			attackType = "ranged";
			rateOfFire = 1.5f;
			//maxChain = 5;
			break;
		case SawedOff:
			hitboxUsedInAttack = rangedWeaponPseudoHitbox;
			attackType = "ranged";
			rateOfFire = 1.5f;
			//maxChain = 5;
			break;
		case Spear:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = spearLightHitbox;
				attackType = "meleeLight";
			} else {
				hitboxUsedInAttack = spearHeavyHitbox;
				attackType = "meleeHeavy";
			}
			break;
		}
	}

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}

	public float GetRateOfFire() {
		return rateOfFire;
	}
}