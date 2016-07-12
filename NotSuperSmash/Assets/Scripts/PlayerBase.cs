using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerBase : MonoBehaviour {
	
	protected float maxHealth;
	protected float knockbackForce;
	protected float attackDamage;

	protected Vector3 moveVector;
	protected Vector3 facingVector;
	protected Vector3 knockbackDirection;

	protected bool isGrounded;

	protected bool isAttacking;
	protected bool isAbleToEquip;
	protected bool isGrappling;
	protected bool isGuarding;

	protected bool isUsingSpecial1;

	protected bool isStaggered;
	protected bool isKnockedBack;
	protected bool isGrappled;

	protected float staggerDuration;
	protected float staggerDurationPassed;

	protected bool isActionInput;
	protected bool isEquipInput;
	protected bool isThrowingInput;
	protected bool isGuardInput;
	protected bool isJumpInput;

	protected bool canMove;
	protected bool canInputActions;

	public float jumpPower;
	public float runSpeed;

	protected float holdInputTime = 0.15f;
	protected float knockbackThreshold = 10;

	List<bool> restrictions;

	// Use this for initialization
	void Start () {
		isGrounded = false;
		restrictions = new List<bool> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IsPlayerGrounded() {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, Vector3.down, out hit, GetComponent<BoxCollider>().bounds.extents.y + 0.1f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		Debug.DrawRay (transform.position, Vector3.down * (GetComponent<BoxCollider>().bounds.extents.y + 0.1f), Color.black);
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

	public bool GetIsGrounded() {
		return isGrounded;
	}

	public void SetIsGrounded(bool isGrounded) {
		this.isGrounded = isGrounded;
	}

	public bool GetIsAbleToEquip() {
		return isAbleToEquip;
	}

	public void SetIsAbleToEquip(bool isAbleToEquip) {
		this.isAbleToEquip = isAbleToEquip;
	}

	public bool GetIsGrappling() {
		return isGrappling;
	}

	public void SetIsGrappling(bool isGrappling) {
		this.isGrappling = isGrappling;
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

	public void SetIsGuarding(bool isGuarding) {
		this.isGuarding = isGuarding;
	}

	public bool GetIsGuardInput() {
		return isGuardInput;
	}

	public void SetIsGuardInput(bool isGuardInput) {
		this.isGuardInput = isGuardInput;
	}

	public bool GetCanMove() {
		return canMove;
	}

	public void SetCanMove(bool canMove) {
		this.canMove = canMove;
	}

	public bool GetCanInputActions() {
		return canInputActions;
	}

	public void SetCanInputActions(bool canInputActions) {
		this.canInputActions = canInputActions;
	}

	public bool GetIsActionInput() {
		return isActionInput;
	}

	public void SetIsActionInput(bool isActionInput) {
		this.isActionInput = isActionInput;
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

	public void SetIsAttacking(bool isAttacking) {
		this.isAttacking = isAttacking;
	}

	public bool GetIsAttacking() {
		return isAttacking;
	}

	public List<bool> GetRestrictions() {
		restrictions = new List<bool> ();
		restrictions.Add(GetIsAttacking ());
		restrictions.Add(GetIsGrappling());
		restrictions.Add(GetIsGuarding());
		restrictions.Add(GetIsUsingSpecial1());
		restrictions.Add(GetIsStaggered());
		restrictions.Add(GetIsKnockedBack());
		restrictions.Add(GetIsGrappled());

		return restrictions;
	}

	public void InterruptActions() {
		SetIsAttacking (false);
		SetIsGrappling(false);
		SetIsUsingSpecial1 (false);
		SetIsGuarding (false);
	}
}
