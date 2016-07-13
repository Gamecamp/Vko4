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
		player.SetIsJumpInput(InputManager.GetButtonInput (gameObject.name, "AButton"));
		player.SetIsGuardInput (InputManager.GetButtonInput (gameObject.name, "LBButton"));
		player.SetIsAction1Input (InputManager.GetButtonInput (gameObject.name, "XButton"));
		player.SetIsAction2Input (InputManager.GetButtonInput (gameObject.name, "YButton"));
		player.SetIsThrowingInput (InputManager.GetButtonInput (gameObject.name, "RBButton"));
		player.SetIsEquipInput (InputManager.GetButtonInput(gameObject.name, "BButton"));
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

//		print (player.GetIsAttacking ());
//		print (player.GetCanMove ());

		if (foundRestriction) {
			player.SetCanMove (false);
			player.SetCanInputActions (false);
		} else {
			player.SetCanMove (true);
			player.SetCanInputActions (true);
		}
	}
}
