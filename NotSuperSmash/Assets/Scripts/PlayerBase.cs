using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {
	private float maxHealth;
	private float knockbackForce;
	private float attackDamage;

	protected bool isJumpInput;
	protected Vector3 moveVector;
	protected bool isGrounded;

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
}
