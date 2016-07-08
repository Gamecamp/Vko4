using UnityEngine;
using System.Collections;

public class PlayerDashSpecial : MonoBehaviour {

	public float dashCooldown;

	private float dashCooldownPassed;
	private float holdInputTime;

	private float buttonHeldTime;

	public float dashSpeed;

	private PlayerMovement player;

	private bool dashStarted;

	private bool dashReady;

	public float dashMaxDuration;
	private float dashPassedDuration;


	Vector3 dashDirection;

	// Use this for initialization
	void Start () {
		buttonHeldTime = 0;
		player = GetComponent<PlayerMovement> ();
		holdInputTime = player.GetHoldInputTime ();
		dashStarted = false;

		dashCooldownPassed = dashCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDashing ();
	}

	void UpdateDashing () {

		if (!dashReady) {
			dashCooldownPassed = dashCooldownPassed + Time.deltaTime;

			if (dashCooldownPassed >= dashCooldown) {
				dashReady = true;
			}
		} 

		if (!dashStarted) {
			if (player.GetIsActionInput () && player.GetCanInputActions () && dashReady) {
				buttonHeldTime = buttonHeldTime + Time.deltaTime;

				print (buttonHeldTime + ", max time: " + holdInputTime);

				if (buttonHeldTime >= holdInputTime) {
					print ("in here");
					dashDirection = new Vector3 (InputManager.GetXInput (this.tag), 0, InputManager.GetYInput (this.tag));
					dashStarted = true;
					player.SetCanMove (false);
					dashReady = false;
					dashCooldownPassed = 0;
				}
			} else {
				buttonHeldTime = 0;
			}
		} else {
			transform.Translate (dashDirection * dashSpeed * Time.deltaTime, Space.World);
			dashPassedDuration = dashPassedDuration + Time.deltaTime;

			if (dashPassedDuration >= dashMaxDuration) {
				player.SetCanMove (true);
				dashStarted = false;
				dashPassedDuration = 0;
			}
		}
	}
}
