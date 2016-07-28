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
	Vector3 facingDirection;

	Vector2 joystickInput;

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
			if (player.GetIsSpecial1Input () && player.GetCanInputActions () && dashReady) {
				joystickInput = InputManager.GetJoystickInput (gameObject.name);
				dashDirection = new Vector3 (joystickInput.x, 0, joystickInput.y);
				dashStarted = true;
				player.SetIsUsingSpecial1 (true);
				dashReady = false;
				dashCooldownPassed = 0;
				
			}
		} else {
			transform.Translate (dashDirection * dashSpeed * Time.deltaTime, Space.World);
			dashPassedDuration = dashPassedDuration + Time.deltaTime;
			facingDirection = new Vector3 (dashDirection.x, 0, dashDirection.z);
			if (facingDirection != Vector3.zero) {
				transform.forward = facingDirection;
			}

			print (dashPassedDuration);

			if (dashPassedDuration >= dashMaxDuration) {
				ResetDash ();
				print ("yolo");
			}
		}
	}

	void ResetDash() {
		dashStarted = false;
		dashPassedDuration = 0;
		player.SetIsUsingSpecial1 (false);
	}
}
