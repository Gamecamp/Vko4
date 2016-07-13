using UnityEngine;
using System.Collections;

public class PlayerUnarmedHeavyAttack : MonoBehaviour {

	PlayerMovement player;
	GameObject unarmedHeavyHitbox;

	private float beforeHurtAnimationLength;
	private float hurtfulAnimationLength;
	private float recoveryTime;
	private float attackDuration;

	private int attackPhaseHelper;

	private bool attackInProgress;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedHeavyHitbox = GameObject.Find ("UnarmedHeavyHitbox" + gameObject.name);
		unarmedHeavyHitbox.SetActive (false);
		attackInProgress = false;

		attackDuration = 0;

		beforeHurtAnimationLength = 0.2f;
		hurtfulAnimationLength = 0.1f;
		recoveryTime = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAttacking ();
	}

	void UpdateAttacking() {

		if (player.GetIsAction2Input () && player.GetCanInputActions ()) {
			player.SetIsHeavyAttacking (true);
			attackInProgress = true;
		}

		if (player.GetIsHeavyAttacking ()) {
			
			attackDuration = attackDuration + Time.deltaTime;

			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
				unarmedHeavyHitbox.SetActive (true);
				attackPhaseHelper++;
			}

			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength && attackPhaseHelper == 1) {
				unarmedHeavyHitbox.SetActive (false);
				attackPhaseHelper++;
			}

			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength + recoveryTime && attackPhaseHelper == 2) {
				ResetAttack ();
			}
		} else if (attackInProgress) {
			ResetAttack ();
		}
	}

	void ResetAttack() {
		player.SetIsHeavyAttacking (false);
		unarmedHeavyHitbox.SetActive (false);
		attackDuration = 0;
		attackPhaseHelper = 0;
		attackInProgress = false;
	}
}
