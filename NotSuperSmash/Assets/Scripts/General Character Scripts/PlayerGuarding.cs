using UnityEngine;
using System.Collections;

public class PlayerGuarding : MonoBehaviour {

	private PlayerMovement player;
	public GameObject guardHitbox;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGuarding ();
	}

	void UpdateGuarding () {
		if (player.GetIsGuardInputOn() && player.GetCanInputActions()) {
			player.SetIsGuarding (true);
			ToggleGuardBarrier (true);
		} else if (player.GetIsGuardInputOff()) {
			player.SetIsGuarding (false);
			ToggleGuardBarrier (false);
		} else {
			//
		}


		//print ("Input = " + player.GetIsGuardInput ());
		//print ("InputManager = " + InputManager.GetButtonInput (gameObject.name, "LBButton"));
	}

	void ToggleGuardBarrier(bool b) {
		guardHitbox.SetActive (b);
	}
}
