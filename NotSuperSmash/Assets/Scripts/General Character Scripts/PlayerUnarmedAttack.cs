using UnityEngine;
using System.Collections;

public class PlayerUnarmedAttack : MonoBehaviour {

	PlayerMovement player;
	GameObject unarmedHitbox;

	// use to control the various animation lengths
	float beforeHurtAnimationLength;
	float hurtfulAnimationLength;
	float recoveryTime;
	float chainResetTime;

	float attackDuration;

	bool attackStarted;


	private int numberInAttackChain;
	private int maxChain;
	private int attackPhaseHelper;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedHitbox = GameObject.Find ("UnarmedHitbox" + gameObject.name);
		unarmedHitbox.SetActive (false);

		numberInAttackChain = 1;
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
		if (player.GetIsActionInput() && player.GetCanInputActions()) {
			attackStarted = true;

		}

		if (attackStarted && player.GetCanInputActions()) {

			//  Attack doesn't hurt yet, swing windup time
			player.SetCanMove (false);
			attackDuration = attackDuration + Time.deltaTime;

			// Attack hurtbox goes on
			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
				unarmedHitbox.SetActive (true);
				attackPhaseHelper++;
			} 

			// Attack has been delivered, still can't move, recovery starts
			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength && attackPhaseHelper == 1) {
				unarmedHitbox.SetActive (false);
				print ("Attack number: " + numberInAttackChain);
				if (numberInAttackChain < maxChain) {
					
					numberInAttackChain++;
				} else {
					ResetAttackChain ();
				}
				attackPhaseHelper++;
			}

			// After recovery control is given back to the player
			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength + recoveryTime && attackPhaseHelper == 2) {
				ResetAttack ();
			}
		}
	}

	void ResetAttack() {
		attackStarted = false;
		attackDuration = 0;
		attackPhaseHelper = 0;

		player.SetCanMove (true);
	}

	void ResetAttackChain() {
		numberInAttackChain = 1;
	}
}
