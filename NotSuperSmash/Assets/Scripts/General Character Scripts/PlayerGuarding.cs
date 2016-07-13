using UnityEngine;
using System.Collections;

public class PlayerGuarding : MonoBehaviour {

	private PlayerMovement player;
	private GameObject guardHitbox;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		guardHitbox = GameObject.Find ("GuardHitbox" + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGuarding ();

	}

	void UpdateGuarding () {
		if (player.GetIsGuardInput() && player.GetCanInputActions()) {
			player.SetIsGuarding (true);
			ToggleGuardBarrier (true);
		} else {
			player.SetIsGuarding (false);
			ToggleGuardBarrier (false);
		}


		//print ("Input = " + player.GetIsGuardInput ());
		//print ("InputManager = " + InputManager.GetButtonInput (gameObject.name, "LBButton"));
	}

	void ToggleGuardBarrier(bool b) {
		guardHitbox.SetActive (b);
	}
}
