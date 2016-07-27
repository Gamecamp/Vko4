using UnityEngine;
using System.Collections;

public class PlayerGuarding : MonoBehaviour {

	private PlayerMovement player;
	private PlayerAnimationHandler animHandler;

	public GameObject guardHitbox;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		animHandler = GetComponent<PlayerAnimationHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGuarding ();
	}

	void UpdateGuarding () {
		if (player.GetIsGuarding () && player.GetCanInputActions ()) {
			player.SetIsGuarding (true);
			ToggleGuardBarrier (true);
			animHandler.SetAnimationBool ("guarding", true);
		} else if (player.GetIsGuarding () == false) {
			player.SetIsGuarding (false);
			ToggleGuardBarrier (false);
			animHandler.SetAnimationBool ("guarding", false);
		}
	}

	void ToggleGuardBarrier(bool b) {
		guardHitbox.SetActive (b);
	}
}
