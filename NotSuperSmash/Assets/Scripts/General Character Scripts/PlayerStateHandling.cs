using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateHandling : MonoBehaviour {

	private PlayerMovement player;

	private List<bool> inputRestrictions;
	private List<bool> inputRestrictionsMove;

	private bool foundInputRestriction;
	private bool foundInputRestrictionMove;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetCanMove (true);
		player.SetCanInputActions (true);

		inputRestrictions = new List<bool> ();
		inputRestrictionsMove = new List<bool> ();
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
		player.SetIsAction1Input (InputManager.GetButtonDownInput (gameObject.name, "XButton"));
		player.SetIsAction2Input (InputManager.GetButtonDownInput (gameObject.name, "YButton"));
		//player.SetIsSpecial1Input
		player.SetIsThrowingInput (InputManager.GetButtonDownInput (gameObject.name, "RBButton"));
		player.SetIsEquipInput (InputManager.GetButtonDownInput(gameObject.name, "BButton"));
	}

	void AlterStates() {
		if (player.GetIsStaggered() || player.GetIsKnockedBack() || player.GetIsGrappled()) {
			player.InterruptActions ();
		}
		GetRestrictions ();
		UpdateStatus ();
	}

	void GetRestrictions() {
		inputRestrictions = player.GetRestrictions ();
		inputRestrictionsMove = player.GetCanInputActionsMoveRestrictions ();
		foundInputRestriction = false;
		foundInputRestrictionMove = false;

		for (int i = 0; i < inputRestrictions.Count; i++) {
			if (inputRestrictions [i]) {
				foundInputRestriction = true;
				break;
			}
		}

		for (int i = 0; i < inputRestrictionsMove.Count; i++) {
			if (inputRestrictionsMove [i]) {
				foundInputRestrictionMove = true;
				break;
			}
		}
	}

	void UpdateStatus() {
		if (foundInputRestriction) {
			player.SetCanMove (false);
			player.SetCanInputActions (false);
		} else if (player.GetIsGuarding ()) {
			player.SetCanMove (false);
			player.SetCanInputActions (true);
		} else {
			player.SetCanMove (true);
			player.SetCanInputActions (true);
		}

		if (foundInputRestrictionMove) {
			player.SetCanInputActionsMove (false);
		} else {
			player.SetCanInputActionsMove (true);
		}
				}
}
