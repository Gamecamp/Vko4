using UnityEngine;
using System.Collections;

public class PlayerStateHandling : MonoBehaviour {

	private PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		GetPlayerInput ();
		AlterStates ();
		//print (player.GetIsGuardInput ());
	}

	void GetPlayerInput() {
		player.SetIsJumpInput(InputManager.GetButtonInput (this.tag, "AButton"));
		player.SetIsGuardInput (InputManager.GetButtonInput (this.tag, "LBButton"));
		player.SetIsActionInput (InputManager.GetButtonInput (this.tag, "XButton"));
		//Check button names below!
		//player.SetIsEquipInput(InputManager.GetButtonInput (this.tag, ""));
		//player.SetIsThrowingInput(InputManager.GetButtonInput (this.tag, ""));
	}

	void AlterStates() {
		if (player.GetIsGuarding ()) {
			player.SetCanMove (false);
		} 

		if (player.GetIsStaggered() || player.GetIsKnockedBack()) {

		}


		if (!player.GetIsGuarding() && player.GetCanInputActions()) {
			player.SetCanMove(true);
		}
	}
}
