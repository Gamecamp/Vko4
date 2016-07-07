using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {
	private float maxHealth;
	private float knockbackForce;
	private float attackDamage;

	protected bool isJumpInput;
	protected Vector3 moveVector;
	protected bool isGrounded;

	protected bool isAbleToEquip;
	protected bool isGrappling;
	protected bool isEquipInput;
	protected bool isThrowingInput;

	protected bool isStaggered;
	protected bool isKnockedBack;
	protected bool isGrappled;

	protected bool isGuarding;
	protected bool isGuardInput;

	public float jumpPower;
	public float runSpeed;

	// Use this for initialization
	void Start () {
		isGrounded = false;
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

	public void SetIsGrabbling(bool isGrappling) {
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
}
