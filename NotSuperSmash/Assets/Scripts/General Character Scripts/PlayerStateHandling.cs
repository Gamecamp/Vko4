using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateHandling : MonoBehaviour {

	private PlayerMovement player;

	private List<bool> restrictions;

	private bool foundRestriction;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetCanMove (true);
		player.SetCanInputActions (true);

		restrictions = new List<bool> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetPlayerInput ();
		AlterStates ();
	}

	void GetPlayerInput() {
		player.SetIsJumpInput(InputManager.GetButtonDownInput (gameObject.name, "AButton"));
		player.SetIsGuardInputOn (InputManager.GetButtonDownInput (gameObject.name, "LBButton"));
		player.SetIsGuardInputOff (InputManager.GetButtonUpInput (gameObject.name, "LBButton"));
		player.SetIsAction1Input (InputManager.GetButtonInput (gameObject.name, "XButton"));
		player.SetIsAction2Input (InputManager.GetButtonDownInput (gameObject.name, "YButton"));
		player.SetIsThrowingInput (InputManager.GetButtonDownInput (gameObject.name, "RBButton"));
		player.SetIsEquipInput (InputManager.GetButtonDownInput(gameObject.name, "BButton"));
	}

	void AlterStates() {

		if (player.GetIsStaggered() || player.GetIsKnockedBack() || player.GetIsGrappled()) {
			player.InterruptActions ();
		}

		restrictions = player.GetRestrictions ();

		foundRestriction = false;

		for (int i = 0; i < restrictions.Count; i++) {
			if (restrictions [i]) {
				foundRestriction = true;
				break;
			}
		}
			
		if (foundRestriction) {
			player.SetCanMove (false);
			player.SetCanInputActions (false);
		} else if (player.GetIsGuarding ()) {
			player.SetCanMove (false);
			player.SetCanInputActions (true);
		} else {
			player.SetCanMove (true);
			player.SetCanInputActions (true);
		}


	}
}
