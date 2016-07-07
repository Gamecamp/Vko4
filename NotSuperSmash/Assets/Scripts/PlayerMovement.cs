using UnityEngine;
using System.Collections;

public class PlayerMovement : PlayerBase {

	// Use this for initialization
	void Start () {
		SetCanInputActions (true);
	}
	
	// Update is called once per frame
	void Update () {
		IsPlayerGrounded ();
		ApplyMovement ();
		ApplyPhysics ();
	}
	void ApplyMovement() {
		if (GetCanMove() && GetCanInputActions()) {
			moveVector += new Vector3(InputManager.GetXInput (this.tag), 0, InputManager.GetYInput (this.tag));

			if (isJumpInput && isGrounded) {
				moveVector = new Vector3(moveVector.x, moveVector.y + jumpPower, moveVector.z);
			}

			transform.Translate (moveVector * runSpeed * Time.deltaTime, Space.World);
		}
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
