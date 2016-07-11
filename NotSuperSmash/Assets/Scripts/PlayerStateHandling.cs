using UnityEngine;
using System.Collections;

public class PlayerStateHandling : MonoBehaviour {

	private PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetCanMove (true);
	}
	
	// Update is called once per frame
	void Update () {
		GetPlayerInput ();
		AlterStates ();
	}

	void GetPlayerInput() {
		player.SetIsJumpInput(InputManager.GetButtonInput (gameObject.name, "AButton"));
		player.SetIsGuardInput (InputManager.GetButtonInput (gameObject.name, "LBButton"));
		player.SetIsActionInput (InputManager.GetButtonInput (gameObject.name, "XButton"));
		player.SetIsThrowingInput (InputManager.GetButtonInput (gameObject.name, "RBButton"));
		player.SetIsEquipInput (InputManager.GetButtonInput(gameObject.name, "BButton"));
	}

	void AlterStates() {
		if (player.GetIsGuarding ()) {
			player.SetCanMove (false);
		} 

		if (player.GetIsStaggered() || player.GetIsKnockedBack()) {

		}


		if (!player.GetIsGuarding() && player.GetCanInputActions()) {
			//player.SetCanMove(true);
		}
	}
}
