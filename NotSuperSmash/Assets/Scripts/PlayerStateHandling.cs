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
	}

	void GetPlayerInput() {
		player.SetIsJumpInput(InputManager.GetButtonInput (this.tag, "AButton"));
		//Check button names below!
		//player.SetIsEquipInput(InputManager.GetButtonInput (this.tag, ""));
		//player.SetIsThrowingInput(InputManager.GetButtonInput (this.tag, ""));
	}
}
