using UnityEngine;
using System.Collections;

public static class InputManager {
	private static float deadZoneAmount = 0.15f;

	public static float GetXInput(string playerTag) {
		string axisName = CheckPlayerNumber (playerTag, "Horizontal");
		float playerXInput = ApplyDeadZone(Input.GetAxis (axisName));

		return playerXInput;
	}

	public static float GetYInput(string playerTag) {
		string axisName = CheckPlayerNumber (playerTag, "Vertical");
		float playerYInput = ApplyDeadZone(Input.GetAxis (axisName));

		return playerYInput;
	}

	public static bool GetButtonInput (string playerTag, string buttonName) {
		buttonName = ("P" + playerTag.Substring (6) + buttonName);

		bool isButtonPressed = false;
		isButtonPressed = Input.GetButtonDown (buttonName);

		return isButtonPressed;
	}

	private static string CheckPlayerNumber(string p, string axis) {
		string axisName = "";

		if (p.Equals ("Player1")) {
			axisName = "P1";
		} else if (p.Equals ("Player2")) {
			axisName = "P2";
		} else if (p.Equals ("Player3")) {
			axisName = "P3";
		} else if (p.Equals ("Player4")) {
			axisName = "P4";
		}

		axisName += axis;

		return axisName;
	}

	private static float ApplyDeadZone(float f) {
		
		if (Mathf.Abs (f) < deadZoneAmount) {
			f = 0;
		}

		return f;
	}
}
