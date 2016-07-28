using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerBase : MonoBehaviour {
	
	protected float maxHealth;
	protected float currentHealth;

	protected int maxLives;
	protected int currentLives;

	protected float knockbackForce;
	protected float attackDamage;

	protected Vector3 moveVector;

	protected Vector3 locationVector;
	protected Vector3 previousLocationVector;
	protected Vector3 knockbackDirection;
	 
	protected Vector3 respawnPoint = new Vector3(0,5,0);

	protected bool isGrounded;
	protected bool deathHandled;

	protected bool isLightAttacking;
	protected bool isHeavyAttacking;
	protected bool isAbleToEquip;
	protected bool isAbleToShoot;

	protected bool isAttemptingGrapple;
	protected bool isGrappling;
	protected bool isGuarding;

	protected bool isBaseballBatEquipped;
	protected bool isPistolEquipped;
	protected bool isShotgunEquipped;
	protected bool isKatanaEquipped;
	protected bool isSawedOffEquipped;
	protected bool isSpearEquipped;

	protected bool isUsingSpecial1;

	protected bool isStaggered;
	protected bool isKnockedBack;
	protected bool isGrappled;

	protected float staggerDuration;
	protected float staggerDurationPassed;

	protected bool isAction1Input;
	protected bool isAction2Input;
	protected bool isSpecial1Input;
	protected bool isEquipInput;
	protected bool isThrowingInput;
	protected bool isGuardInputOn;
	protected bool isGuardInputOff;
	protected bool isJumpInput;

	protected bool canMove;
	protected bool canInputActionsMove;
	protected bool canInputActions;

	protected float velocity;

	public float jumpPower;
	public float runSpeed;

	Rigidbody rb;

	protected float holdInputTime = 0.15f;
	protected float knockbackThreshold = 10;

	List<bool> canInputActionsRestrictions = new List<bool> ();
	List<bool> canInputActionsMoveRestrictions = new List<bool>();

	public void IsPlayerGrounded() {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, Vector3.down, out hit, GetComponent<Collider>().bounds.extents.y + 10)) {
			print ("GROUNDED");
			isGrounded = true;
		} else {
			print ("NOT FUCKING GROUNDED");
			isGrounded = false;
		}

		Debug.DrawRay (transform.position, Vector3.down * (GetComponent<BoxCollider>().bounds.extents.y + 0.1f), Color.black);
	}

	public void SetRigidbody(Rigidbody rb) {
		this.rb = rb;
	}

	public bool GetIsJumpInput() {
		return isJumpInput;
	}

	public void SetIsJumpInput(bool isJumpImput) {
		this.isJumpInput = isJumpImput;
	}

	public Vector3 GetMoveVector() {
		return moveVector;
	}

	public void SetMoveVector(Vector3 moveVector) {
		this.moveVector = moveVector;
	}

	public Vector3 GetPreviousLocationVector() {
		return previousLocationVector;
	}

	public void SetPreviousLocationVector(Vector3 previousLocationVector) {
		this.previousLocationVector = previousLocationVector;	
	}
	public bool GetIsGrounded() {
		return isGrounded;
	}

	public void SetIsGrounded(bool isGrounded) {
		this.isGrounded = isGrounded;
	}

	public bool GetDeathHandled() {
		return deathHandled;
	}

	public void SetDeathHandled(bool deathHandled) {
		this.deathHandled = deathHandled;
	}

	public bool GetIsAbleToEquip() {
		return isAbleToEquip;
	}

	public void SetIsAbleToEquip(bool isAbleToEquip) {
		this.isAbleToEquip = isAbleToEquip;
	}

	public bool GetIsAbleToShoot() {
		return isAbleToShoot;
	}

	public void SetIsAbleToShoot(bool isAbleToShoot) {
		this.isAbleToShoot = isAbleToShoot;
	}

	public bool GetIsGrappling() {
		return isGrappling;
	}

	public void SetIsGrappling(bool isGrappling) {
		this.isGrappling = isGrappling;
	}

	public bool GetIsAttemptingGrapple() {
		return isAttemptingGrapple;
	}

	public void SetIsAttemptingGrapple(bool isAttemptingGrapple) {
		this.isAttemptingGrapple = isAttemptingGrapple;
	}

	public bool GetIsEquipInput() {
		return isEquipInput;
	}

	public void SetIsEquipInput(bool isEquipInput) {
		this.isEquipInput = isEquipInput;
	}

	public bool GetIsThrowingInput() {
		return isThrowingInput;
	}

	public void SetIsThrowingInput(bool isThrowingInput) {
		this.isThrowingInput = isThrowingInput;
	}

	public bool GetIsStaggered() {
		return isStaggered;
	}

	public void SetIsStaggered(bool isStaggered) {
		this.isStaggered = isStaggered;
	}

	public bool GetIsKnockedBack() {
		return isKnockedBack;
	}

	public void SetIsKnockedBack(bool isKnockedBack) {
		this.isKnockedBack = isKnockedBack;
	}

	public bool GetIsGrappled() {
		return isGrappled;
	}

	public void SetIsGrappled(bool isGrappled) {
		this.isGrappled = isGrappled;
	}

	public bool GetIsGuarding() {
		return isGuarding;
	}

	public void SetIsGuarding (bool isGuarding) {
		this.isGuarding = isGuarding;
	}

	public void SetIsGuardInputOn(bool isGuardInputOn) {
		this.isGuardInputOn = isGuardInputOn;
	}

	public void SetIsGuardInputOff(bool isGuardInputOff) {
		this.isGuardInputOff = isGuardInputOff;
	}

	public bool GetCanMove() {
		return canMove;
	}

	public void SetCanMove(bool canMove) {
		this.canMove = canMove;
	}

	public bool GetCanInputActionsMove() {
		return canInputActionsMove;
	}

	public void SetCanInputActionsMove(bool canInputActionsMove) {
		this.canInputActionsMove=canInputActionsMove;
	}

	public bool GetCanInputActions() {
		return canInputActions;
	}

	public void SetCanInputActions(bool canInputActions) {
		this.canInputActions = canInputActions;
	}

	public bool GetIsAction1Input() {
		return isAction1Input;
	}

	public void SetIsAction1Input(bool isAction1Input) {
		this.isAction1Input = isAction1Input;
	}

	public bool GetIsAction2Input() {
		return isAction2Input;
	}

	public void SetIsAction2Input(bool isAction2Input) {
		this.isAction2Input = isAction2Input;

	}

	public bool GetIsSpecial1Input() {
		return isSpecial1Input;
	}

	public void SetIsSpecial1Input(bool isSpecial1Input) {
		this.isSpecial1Input = isSpecial1Input;
	}

	public bool GetIsUsingSpecial1() {
		return isUsingSpecial1;
	}

	public void SetIsUsingSpecial1(bool isUsingSpecial1) {
		this.isUsingSpecial1 = isUsingSpecial1;
	}

	public float GetHoldInputTime() {
		return holdInputTime;
	}

	public void SetStaggerDuration(float staggerDuration) {
		this.staggerDuration = staggerDuration;
	}

	public float GetStaggerDuration() {
		return staggerDuration;
	}

	public void SetStaggerDurationPassed(float staggerDurationPassed) {
		this.staggerDurationPassed = staggerDurationPassed;
	}

	public float GetStaggerDurationPassed() {
		return staggerDurationPassed;
	}

	public void SetIsLightAttacking(bool isLightAttacking) {
		this.isLightAttacking = isLightAttacking;
	}

	public bool GetIsLightAttacking() {
		return isLightAttacking;
	}

	public void SetIsHeavyAttacking(bool isHeavyAttacking) {
		this.isHeavyAttacking = isHeavyAttacking;
	}

	public bool GetIsHeavyAttacking() {
		return isHeavyAttacking;
	}

	public void SetIsBaseballBatEquipped(bool isBaseballBatEquipped) {
		this.isBaseballBatEquipped = isBaseballBatEquipped;
	}

	public bool GetIsBaseballBatEquipped() {
		return isBaseballBatEquipped;
	}

	public void SetIsPistolEquipped(bool isPistolEquipped) {
		this.isPistolEquipped = isPistolEquipped;
	}

	public bool GetIsPistolEquipped() {
		return isPistolEquipped;
	}

	public void SetIsSawedOffEquipped(bool isSawedOffEquipped) {
		this.isSawedOffEquipped = isSawedOffEquipped;
	}

	public bool GetIsSawedOffEquipped() {
		return isSawedOffEquipped;
	}

	public void SetIsShotgunEquipped(bool isShotgunEquipped) {
		this.isShotgunEquipped = isShotgunEquipped;
	}

	public bool GetIsShotgunEquipped() {
		return isShotgunEquipped;
	}

	public void SetIsKatanaEquipped(bool isKatanaEquipped) {
		this.isKatanaEquipped = isKatanaEquipped;
	}

	public bool GetIsKatanaEquipped() {
		return isKatanaEquipped;
	}

	public void SetIsSpearEquipped(bool isSpearEquipped) {
		this.isSpearEquipped = isSpearEquipped;
	}

	public bool GetIsSpearEquipped() {
		return isSpearEquipped;
	}

	public float GetMaxHealth() {
		return maxHealth;
	}

	public float GetCurrentHealth () {
		return currentHealth;
	}

	public void SetCurrentHealth(float currentHealth) {
		this.currentHealth = currentHealth;
	}

	public int GetMaxLives() {
		return maxLives;
	}

	public void SetMaxLives(int maxLives) {
		this.maxLives = maxLives;
	}

	public int GetCurrentLives() {
		return currentLives;
	}

	public void SetCurrentLives(int currentLives) {
		this.currentLives = currentLives;
	}

	public void DecreaseHealth(float damage) {
		if (currentHealth - damage <= 0) {
			currentHealth = 0;
		} else {
			currentHealth -= damage;
		}
	}

	public float GetAttackDamage() {
		return attackDamage;
	}

	public void SetAttackDamage(float attackDamage) {
		this.attackDamage = attackDamage;
	}

	public float GetVelocity() {
		return velocity;
	}

	public void SetVelocity(float velocity) {
		this.velocity = velocity;
	}
		
	protected void ResetStatus() {
		currentHealth = maxHealth;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = respawnPoint;

		isLightAttacking = false;
		isHeavyAttacking = false;
		isGrappling = false;
		isGuarding = false;

		isBaseballBatEquipped = false;

		isUsingSpecial1 = false;

		isStaggered = false;
		isKnockedBack = false;
		isGrappled = false;

		staggerDuration = 0;
		staggerDurationPassed = 0;

		canMove = true;
		canInputActions = true;

	}

	public List<bool> GetRestrictions() {
		canInputActionsRestrictions.Clear ();
		canInputActionsRestrictions.Add(GetIsStaggered());
		canInputActionsRestrictions.Add(GetIsKnockedBack());
		canInputActionsRestrictions.Add(GetIsGrappled());
		canInputActionsRestrictions.Add (GetIsGrappling ());
		return canInputActionsRestrictions;
	}

	public List<bool> GetCanInputActionsMoveRestrictions() {
		canInputActionsMoveRestrictions.Clear ();
		canInputActionsMoveRestrictions.Add(GetIsLightAttacking());
		canInputActionsMoveRestrictions.Add(GetIsHeavyAttacking());
		canInputActionsMoveRestrictions.Add(GetIsAttemptingGrapple());
		canInputActionsMoveRestrictions.Add(GetIsUsingSpecial1());

		return canInputActionsMoveRestrictions;
	}

	public void PrintStatus() {

	}
		

	public void InterruptActions() {
		SetIsLightAttacking (false);
		SetIsHeavyAttacking (false);
		SetIsAttemptingGrapple (false);
		SetIsGrappling(false);
		SetIsUsingSpecial1 (false);
		SetIsGuarding (false);
	}
}
