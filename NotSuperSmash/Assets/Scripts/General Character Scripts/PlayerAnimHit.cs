using UnityEngine;
using System.Collections;

public class PlayerAnimHit : MonoBehaviour {
	public PlayerMovement player;
	public GameObject hitbox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateHitbox() {
		hitbox.SetActive (true);
	}

	public void DeactivateHitbox() {
		hitbox.SetActive (false);
	}
}
