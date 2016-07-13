using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {

	PlayerMovement player;
	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;

	GameObject hitboxUsedInAttack;

	// use to control the various animation lengths
	private float beforeHurtAnimationLength;
	private float hurtfulAnimationLength;
	private float recoveryTime;
	private float attackDuration;

	private float chainResetTime;

	private int numberInAttackChain;
	private int maxChain;
	private int attackPhaseHelper;

	private bool attackInProgress;

	private string activeWeapon;

	const string unarmed = "unarmed";
	const string baseballBat = "baseballBat";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);
		attackInProgress = false;

		activeWeapon = unarmed;

		numberInAttackChain = 1;

		attackPhaseHelper = 0;
		attackDuration = 0;
	}

	// Update is called once per frame
	void Update () {
		UpdateAttacking ();
	}

	void UpdateAttacking () {

		if (player.GetIsAction1Input() && player.GetCanInputActions() && !attackInProgress) {
			player.SetIsLightAttacking (true);
			DetermineAttackProperties();
			attackInProgress = true;
		} else if (player.GetIsAction2Input() && player.GetCanInputActions() && !attackInProgress) {
			player.SetIsHeavyAttacking (true);
			DetermineAttackProperties();
			attackInProgress = true;
		}

		if (player.GetIsLightAttacking () || player.GetIsHeavyAttacking ()) {

			//  Attack doesn't hurt yet, swing windup time
			attackDuration = attackDuration + Time.deltaTime;

			// Attack hurtbox goes on
			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
				hitboxUsedInAttack.SetActive (true);
				attackPhaseHelper++;
			} 

			// Attack has been delivered, still can't move, recovery starts
			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength && attackPhaseHelper == 1) {
				hitboxUsedInAttack.SetActive (false);
				if (maxChain > 1) {
					if (numberInAttackChain < maxChain) {
						numberInAttackChain++;
					} else {
						ResetAttackChain ();
					}
				}
				attackPhaseHelper++;
			}

			// After recovery control is given back to the player
			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength + recoveryTime && attackPhaseHelper == 2) {
				ResetAttack ();
			}
		} else if (attackInProgress) {
			ResetAttack ();
		}
	}

	void DetermineAttackProperties() {
		switch (activeWeapon) {
		case unarmed:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = unarmedLightHitbox;

				beforeHurtAnimationLength = 0.1f;
				hurtfulAnimationLength = 0.1f;
				recoveryTime = 0.05f;

				maxChain = 3;
			} else {
				hitboxUsedInAttack = unarmedHeavyHitbox;

				beforeHurtAnimationLength = 0.2f;
				hurtfulAnimationLength = 0.2f;
				recoveryTime = 0.1f;

				maxChain = 1;
			}
			break;
		case baseballBat:
			if (player.GetIsLightAttacking()) {
				hitboxUsedInAttack = baseballBatLightHitbox;

				beforeHurtAnimationLength = 0.3f;
				hurtfulAnimationLength = 0.4f;
				recoveryTime = 0.5f;

				maxChain = 3;
			} else {
				hitboxUsedInAttack = baseballBatHeavyHitbox;

				beforeHurtAnimationLength = 0.7f;
				hurtfulAnimationLength = 0.8f;
				recoveryTime = 0.9f;

				maxChain = 1;
			}
			break;
		}
	}

	void ResetAttack() {
		player.SetIsLightAttacking (false);
		player.SetIsHeavyAttacking (false);
		hitboxUsedInAttack.SetActive (false);
		attackDuration = 0;
		attackPhaseHelper = 0;
		attackInProgress = false;
	}

	void ResetAttackChain() {
		numberInAttackChain = 1;
	}

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}
}
