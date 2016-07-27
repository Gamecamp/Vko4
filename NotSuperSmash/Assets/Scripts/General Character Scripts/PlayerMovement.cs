using UnityEngine;
using System.Collections;

public class PlayerMovement : PlayerBase {

	Rigidbody rigidBody;

	private bool knockbackType;

	private bool respawning;
	private bool invulnerable;

	private float respawnTime = 3;
	private float respawnTimePassed = 0;
	private float invulnerableTime = 3;
	private float invulnerableTimePassed = 0;

	float velocity;

	Vector2 joystickInput;
	Vector2 oldJoystickInput;

	Vector3 facingVector;
	Vector3 movementDifference;

	Renderer renderer;

	private float step;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		maxHealth = 100;
		currentHealth = 100;
		attackDamage = 50;
		maxLives = 3;
		currentLives = maxLives;
		SetRigidbody (rigidBody);
		renderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		IsPlayerGrounded ();
		CorrectAngle ();
		ApplyMovement ();
		ApplyPhysics ();
		ApplyKnockbacks ();
		ApplyStagger ();
		CountVelocity ();
		HandleRespawn ();
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

	void CountVelocity() {
		movementDifference = (new Vector3 (transform.position.x, 0, transform.position.z) - new Vector3(GetPreviousLocationVector().x, 0, GetPreviousLocationVector().z)) /Time.deltaTime;
		SetVelocity(movementDifference.magnitude);
	}

	void CorrectAngle () {
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); 
	}

	void ApplyMovement() {

		SetPreviousLocationVector (transform.position);

		if (GetCanMove() && GetCanInputActions() && GetCanInputActionsMove()) {
			GetInputForMoving ();
			transform.Translate (moveVector * runSpeed * Time.deltaTime, Space.World);
			RotateCharacter ();
		}

		if (GetCanMove () && !GetCanInputActionsMove ()) {
			moveVector += new Vector3 (oldJoystickInput.x, 0, oldJoystickInput.y);
			oldJoystickInput.x = oldJoystickInput.x * 0.95f;
			oldJoystickInput.y = oldJoystickInput.y * 0.95f;


			transform.Translate (moveVector * runSpeed * Time.deltaTime, Space.World);
			RotateCharacter ();
		}
	}

	void GetInputForMoving() {
		oldJoystickInput = joystickInput;
		joystickInput = InputManager.GetJoystickInput (gameObject.name).normalized;
		moveVector += new Vector3 (joystickInput.x, 0, joystickInput.y);
		if (isJumpInput && isGrounded) {
			moveVector = new Vector3(moveVector.x, moveVector.y + jumpPower, moveVector.z);
		}
	}

	void RotateCharacter() {
		step = Time.deltaTime;
		facingVector = new Vector3 (moveVector.x, 0, moveVector.z);
		if (facingVector != Vector3.zero) {
			transform.forward = Vector3.RotateTowards(facingVector, facingVector, step, 0.0F);
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

	void HandleRespawn() {
		if (respawning) {

			respawnTimePassed = respawnTimePassed + Time.deltaTime;

			if (respawnTimePassed > respawnTime) {
				invulnerable = true;
				StartCoroutine (Blink ());
				respawning = false;
				respawnTimePassed = 0;
				ResetStatus ();
			}
		}

		if (invulnerable) {
			gameObject.tag = "Invulnerable";

			invulnerableTimePassed = invulnerableTimePassed + Time.deltaTime;

			if (invulnerableTimePassed >= invulnerableTime) {
				invulnerable = false;
				invulnerableTimePassed = 0;
				gameObject.tag = "Player";
			}
		}
	}

	public void Respawn() {
		respawning = true;
	}

	public void Kill() {
		SetIsStaggered (true);
		SetStaggerDuration (1000f);
	}

	IEnumerator Blink() {
		float endTime = Time.time + invulnerableTime;
		while (Time.time < endTime) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.1f);
			renderer.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
	}


	void ApplyPhysics() {
		MyPhysics.ApplyFriction (this);

//		if (!GetIsGrounded()) {
//			MyPhysics.ApplyGravity (this.gameObject);
//		}
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
