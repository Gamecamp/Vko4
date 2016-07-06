using UnityEngine;
using System.Collections;

public class PlayerMovement : PlayerBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		IsPlayerGrounded ();
		GetPlayerInput ();
		ApplyMovement ();
		ApplyPhysics ();
	}

	void GetPlayerInput () {
		moveVector += new Vector3(InputManager.GetXInput (this.tag), 0, InputManager.GetYInput (this.tag));
		isJumpInput = InputManager.GetButtonInput (this.tag, "AButton");
	}

	void ApplyMovement() {
		
		if (isJumpInput && isGrounded) {
			moveVector = new Vector3(moveVector.x, moveVector.y + jumpPower, moveVector.z);
		}
		
		transform.Translate (moveVector * runSpeed * Time.deltaTime, Space.World);
	}

	void ApplyPhysics() {
		MyPhysics.ApplyFriction (this);
	}

	public Vector3 GetMoveVector() {
		return moveVector;
	}

	public void SetMoveVector(Vector3 v) {
		moveVector = v;
	}
}
