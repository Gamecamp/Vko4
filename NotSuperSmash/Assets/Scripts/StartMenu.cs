using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Animator pressAnyButtonAnimator;
	public Animator playerSelectionAnimator;

	private bool startUpMode;
	private bool playerSelectionMode;

	// Use this for initialization
	void Start () {
		startUpMode = true;
		playerSelectionMode = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if (startUpMode && Input.anyKeyDown) {
			StartPlayerSelectionMode ();
		}
	}

	void StartPlayerSelectionMode() {
		startUpMode = false;
		playerSelectionMode = true;
		pressAnyButtonAnimator.SetBool ("buttonPressed", true);
		playerSelectionAnimator.SetBool ("slideIn", true);
	}
}
