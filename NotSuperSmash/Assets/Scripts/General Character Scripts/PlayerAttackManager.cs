using UnityEngine;
using System.Collections;

public class PlayerAttackManager : MonoBehaviour {

	private PlayerMovement player;
	public GameObject unarmedLightHitbox;
	public GameObject unarmedHeavyHitbox;
	public GameObject baseballBatLightHitbox;
	public GameObject baseballBatHeavyHitbox;

	private GameObject hitboxUsedInAttack;

	private int currentCombo;
	private int maxCombo;
	private string activeWeapon;
	private bool attackInProgress;

	public enum AttackState {startPhase, damagePhase, comboPhase, recoveryPhase};
	public AttackState attackPhase;
	private bool action1Input;
	private bool action2Input;
	private Animator anim;
	private PlayerAnimationHandler animHandler;
	private bool isNextComboHit;

	private string attackType;
	const string unarmed = "unarmed";
	const string baseballBat = "baseballBat";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		unarmedLightHitbox.SetActive (false);
		unarmedHeavyHitbox.SetActive (false);
		baseballBatLightHitbox.SetActive (false);
		baseballBatHeavyHitbox.SetActive (false);

		activeWeapon = unarmed;
		currentCombo = 1;

		attackPhase = AttackState.startPhase;
		animHandler = GetComponent<PlayerAnimationHandler> ();

		hitboxUsedInAttack = unarmedLightHitbox;
	}

	// Update is called once per frame
	void Update () {
		UpdateAttacking ();


	}

	void UpdateAttacking () {

		if (player.GetIsAction1Input () && player.GetCanInputActions ()) {
			action1Input = true;
			player.SetIsLightAttacking (true);
			attackInProgress = true;
		} else if (player.GetIsAction2Input () && player.GetCanInputActions ()) {
			action2Input = true;
			player.SetIsHeavyAttacking (true);
			attackInProgress = true;
		}

		if (player.gameObject.name == "Player1") {
			print ("light " + player.GetIsLightAttacking ());
			print ("heavy " + player.GetIsHeavyAttacking ());
			print (currentCombo);
			print (attackPhase);
			print ("isNextCombo " + isNextComboHit);
			isNextComboHit = false;
		}

		if (player.GetIsLightAttacking () || player.GetIsHeavyAttacking ()) {
			if (attackPhase == AttackState.startPhase) {
				if (action1Input) {
					action1Input = false;
					DetermineAttackProperties ();
					animHandler.SetAnimationTrigger (attackType, currentCombo, true);
				} else if (action2Input) {
					action2Input = false;
					DetermineAttackProperties ();
					animHandler.SetAnimationTrigger (attackType, currentCombo, true);
				}
			} else if (attackPhase == AttackState.damagePhase) {
				hitboxUsedInAttack.SetActive (true);
				animHandler.ResetComboTriggers ();
			} else if (attackPhase == AttackState.comboPhase) {
				hitboxUsedInAttack.SetActive (false);
				isNextComboHit = false;
				ComboHit ();
			} else if (attackPhase == AttackState.recoveryPhase && !isNextComboHit || currentCombo == maxCombo) {
				ResetAttack ();
				ResetAttackChain ();
			} 
		} else {
			ResetAttack ();
		}
	}

	void ComboHit() {
		if((currentCombo != maxCombo) && (action1Input || action2Input)) {
			attackPhase = AttackState.startPhase;
			currentCombo++;
			isNextComboHit = true;
		}
	}

	void DetermineAttackProperties() {
		switch (activeWeapon) {
		case unarmed:
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
		case baseballBat:
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
		if (toAttackPhase.Equals ("damagePhase")) {
			this.attackPhase = AttackState.damagePhase;
		} else if (toAttackPhase.Equals ("comboPhase")) {
			this.attackPhase = AttackState.comboPhase;
		} else if (toAttackPhase.Equals ("recoveryPhase")) {
			this.attackPhase = AttackState.recoveryPhase;
		}
	}

	void ResetAttack() {
		player.SetIsLightAttacking (false);
		player.SetIsHeavyAttacking (false);
		hitboxUsedInAttack.SetActive (false);
		attackInProgress = false;
		attackPhase = AttackState.startPhase;
		currentCombo = 1;
		isNextComboHit = false;
	}

	void ResetAttackChain() {
		currentCombo = 1;
	}

	public void SetActiveWeapon(string weapon) {
		activeWeapon = weapon;
	}
		
	//		if (attackPhase == AttackState.setUpPhase) {
	//			if (player.GetIsAction1Input () && player.GetCanInputActions () && !attackInProgress) {
	//				player.SetIsLightAttacking (true);
	//				DetermineAttackProperties ();
	//				attackInProgress = true;
	//			} else if (player.GetIsAction2Input () && player.GetCanInputActions () && !attackInProgress) {
	//				player.SetIsHeavyAttacking (true);
	//				DetermineAttackProperties ();
	//				attackInProgress = true;
	//			}
	//		}
	//
	//		if (player.GetIsLightAttacking () || player.GetIsHeavyAttacking ()) {
	//
	//			//  Attack doesn't hurt yet, swing windup time
	//			attackDuration = attackDuration + Time.deltaTime;
	//
	//			// Attack hurtbox goes on
	//			if (attackDuration >= beforeHurtAnimationLength && attackPhaseHelper == 0) {
	//				hitboxUsedInAttack.SetActive (true);
	//				attackPhaseHelper++;
	//			} 
	//
	//			// Attack has been delivered, still can't move, recovery starts
	//			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength && attackPhaseHelper == 1) {
	//				hitboxUsedInAttack.SetActive (false);
	//				if (maxChain > 1) {
	//					if (numberInAttackChain < maxChain) {
	//						numberInAttackChain++;
	//					} else {
	//						ResetAttackChain ();
	//					}
	//				}
	//				attackPhaseHelper++;
	//			}
	//
	//			// After recovery control is given back to the player
	//			if (attackDuration >= beforeHurtAnimationLength + hurtfulAnimationLength + recoveryTime && attackPhaseHelper == 2) {
	//				ResetAttack ();
	//			}
	//		} else if (attackInProgress) {
	//			ResetAttack ();
	//		}
}
