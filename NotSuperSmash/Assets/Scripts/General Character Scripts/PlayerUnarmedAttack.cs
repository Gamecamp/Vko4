using UnityEngine;
using System.Collections;

public class PlayerUnarmedAttack : MonoBehaviour {

	PlayerMovement player;
	GameObject unarmedHitbox;

	// use to control the various animation lengths
	private float beforeHurtAnimationLength;
	private float hurtfulAnimationLength;
	private float recoveryTime;
	private float chainResetTime;
	private float attackDuration;

	private int numberInLightAttackChain;
	private int maxChain;
	private int attackPhaseHelper;

	private bool attackInProgress;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedHitbox = GameObject.Find ("UnarmedHitbox" + gameObject.name);
		unarmedHitbox.SetActive (false);
		attackInProgress = false;

		numberInLightAttackChain = 1;
		attackPhaseHelper = 0;
		maxChain = 3;

		attackDuration = 0;

		beforeHurtAnimationLength = 0.1f;
		hurtfulAnimationLength = 0.1f;
		recoveryTime = 0.05f;
		chainResetTime = recoveryTime;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAttacking ();
	}

	void UpdateAttacking () {

		if (player.GetIsAction1Input() && player.GetCanInputActions()) {
			player.SetIsLightAttacking (true);
			attackInProgress = true;
		}
			

		if (player.GetIsLightAttacking ()) {

			//  Attack doesn't hurt yet, swing windup time
			attackDuration = attackDuration + Time.deltaTime;

			// Attack hurtbox goes on
			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
				unarmedHitbox.SetActive (true);
				attackPhaseHelper++;
			} 

			// Attack has been delivered, still can't move, recovery starts
			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength && attackPhaseHelper == 1) {
				unarmedHitbox.SetActive (false);
				if (numberInLightAttackChain < maxChain) {
					
					numberInLightAttackChain++;
				} else {
					ResetAttackChain ();
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

	void ResetAttack() {
		player.SetIsLightAttacking (false);
		unarmedHitbox.SetActive (false);
		attackDuration = 0;
		attackPhaseHelper = 0;
		attackInProgress = false;
	}

	void ResetAttackChain() {
		numberInLightAttackChain = 1;
	}
}
