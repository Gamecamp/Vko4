using UnityEngine;
using System.Collections;

public class PlayerGrapple : MonoBehaviour {

	PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
	}

	// Update is called once per frame
	void Update () {
		UpdateGrappling ();
	}

	void UpdateGrappling () {
		if (player.GetIsGrappling() && player.GetCanInputActions()) {
			player.SetIsGuarding (true);
		} else {
			player.SetIsGuarding (false);
		}
	}
}
