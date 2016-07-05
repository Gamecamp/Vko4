using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public Vector3 moveVector;
	public float moveSpeed;
	public float jumpPower;
		
	private bool isJump;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetPlayerInput ();
		ApplyMovement ();
		ApplyPhysics ();
	}

	void GetPlayerInput () {
		isJump = InputManager.GetButtonInput (this.tag, "AButton");
		moveVector += new Vector3(InputManager.GetXInput (this.tag), 0, InputManager.GetYInput (this.tag));
	}

	void ApplyMovement() {
		if (isJump) {
			moveVector = new Vector3(moveVector.x, moveVector.y + jumpPower, moveVector.z);
		}
		
		transform.Translate (moveVector * moveSpeed * Time.deltaTime, Space.World);
	}

	void ApplyPhysics() {
		//Physics.ApplyGravity(gameObject);
		Physics.ApplyFriction(this);
	}
}
