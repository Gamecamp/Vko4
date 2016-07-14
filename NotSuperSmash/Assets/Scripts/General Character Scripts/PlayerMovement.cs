using UnityEngine;
using System.Collections;

public class PlayerMovement : PlayerBase {

	Rigidbody rigidBody;

	private bool knockbackType;

	Vector2 joystickInput;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		maxHealth = 100;
		currentHealth = 100;
		attackDamage = 50;
		maxLives = 3;
		currentLives = maxLives;
		SetRigidbody (rigidBody);
	}
	
	// Update is called once per frame
	void Update () {
		IsPlayerGrounded ();
		CorrectAngle ();
		ApplyMovement ();
		ApplyPhysics ();
		ApplyKnockbacks ();
		ApplyStagger ();
	}

	void ApplyKnockbacks() {
		if (isKnockedBack) {
			transform.Translate (knockbackDirection * knockbackForce * Time.deltaTime, Space.World);
			knockbackForce--;

			if (knockbackForce < knockbackThreshold) {
				SetIsKnockedBack (false);
			}
		}
	}

	void CorrectAngle () {
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); 
	}


	void ApplyMovement() {
		if (GetCanMove() && GetCanInputActions()) {
			joystickInput = InputManager.GetJoystickInput (gameObject.name);
			moveVector += new Vector3 (joystickInput.x, 0, joystickInput.y);
			if (isJumpInput && isGrounded) {
				moveVector = new Vector3(moveVector.x, moveVector.y + jumpPower, moveVector.z);
			}
			transform.Translate (moveVector * runSpeed * Time.deltaTime, Space.World);
			facingVector = new Vector3 (moveVector.x, 0, moveVector.z);
			if (facingVector != Vector3.zero) {
				transform.forward = facingVector;
			}
		}
	}

	public void StartKnockback(Vector3 direction, float force) {
		SetIsKnockedBack(true);
		knockbackForce = force;
		knockbackDirection = direction;
	}
		

	public void StartStagger(float staggerDuration) {
		SetIsStaggered (true);
		SetStaggerDuration (staggerDuration);
	}


	void ApplyPhysics() {
		MyPhysics.ApplyFriction (this);
	}

	void ApplyStagger() {
		if (GetIsStaggered ()) {
			SetStaggerDurationPassed (GetStaggerDurationPassed () + Time.deltaTime);

			if (GetStaggerDurationPassed() >= GetStaggerDuration()) {
				SetIsStaggered(false);
				SetStaggerDurationPassed(0);
				SetStaggerDuration(0);
			}
		}
	}
		

	public void LookTowards(Vector3 v) {
		transform.LookAt (v);
	}
}
