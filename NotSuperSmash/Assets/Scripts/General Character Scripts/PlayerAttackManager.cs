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

	private int currentCombo;
	private int maxCombo;
	private int attackPhaseHelper;

	private bool attackInProgress;

	private string activeWeapon;

	public enum AttackState {startPhase, damagePhase, comboPhase, recoveryPhase};
	public AttackState attackPhase;
	private bool isAttackHitbox;
	private bool action1Input;
	private bool action2Input;
	private Animator anim;
	private PlayerAnimationHandler animHandler;

	private string animationComboOdd;
	private string animationComboEven;
	private string animationComboFinal;

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

		currentCombo = 1;

		attackPhaseHelper = 0;
		attackDuration = 0;

		attackPhase = AttackState.startPhase;
		animHandler = GetComponent<PlayerAnimationHandler> ();
	}

	// Update is called once per frame
	void Update () {
		UpdateAttacking ();
		print (attackPhase);
	}

	void UpdateAttacking () {

		if (player.GetIsAction1Input () && player.GetCanInputActions ()) {
			action1Input = true;
		} else if (player.GetIsAction2Input () && player.GetCanInputActions ()) {
			action2Input = true;
		}
			
		if (attackPhase == AttackState.startPhase) {
			if (action1Input) {
				action1Input = false;
				player.SetIsLightAttacking (true);
				DetermineAttackProperties ();
				DetermineRightAnimation ();
			} else if (action2Input) {
				action2Input = false;
				player.SetIsHeavyAttacking (true);
				DetermineAttackProperties ();
				//anim.start/play( DetermineRightAnimation (hitboxUsedInAttack.name) , true);
			}
		} else if (attackPhase == AttackState.damagePhase) {
			hitboxUsedInAttack.SetActive (true);
			animHandler.ResetComboAnimations ();
		} else if (attackPhase == AttackState.comboPhase) {
			hitboxUsedInAttack.SetActive (false);
			ComboHit ();
		} else if (attackPhase == AttackState.recoveryPhase) {
			ResetAttack ();
			ResetAttackChain ();
			print ("reset");
		}
	}

	void DetermineRightAnimation() {
		if (currentCombo == 1) {
			animHandler.SetCombo1 (true);
		} else if (currentCombo == 2) {
			animHandler.SetCombo2 (true);
		} else if (currentCombo == maxCombo) {
			//animHandler.SetCombo3 (true);
		}
	}

	void ComboHit() {
		if((currentCombo != maxCombo) && (action1Input || action2Input)) {
			attackPhase = AttackState.startPhase;
			hitboxUsedInAttack.SetActive (false);
			currentCombo++;
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

					maxCombo = 3;
				} else {
					hitboxUsedInAttack = unarmedHeavyHitbox;

					beforeHurtAnimationLength = 0.2f;
					hurtfulAnimationLength = 0.2f;
					recoveryTime = 0.1f;

					maxCombo = 1;
				}
				break;
			case baseballBat:
				if (player.GetIsLightAttacking()) {
					hitboxUsedInAttack = baseballBatLightHitbox;

					beforeHurtAnimationLength = 0.3f;
					hurtfulAnimationLength = 0.4f;
					recoveryTime = 0.5f;

					maxCombo = 3;
				} else {
					hitboxUsedInAttack = baseballBatHeavyHitbox;

					beforeHurtAnimationLength = 0.7f;
					hurtfulAnimationLength = 0.8f;
					recoveryTime = 0.9f;

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
		attackDuration = 0;
		attackPhaseHelper = 0;
		attackInProgress = false;
	}

	void ResetAttackChain() {
		currentCombo = 1;
		attackPhase = AttackState.startPhase;
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
