using UnityEngine;
using System.Collections;

public static class InputManager {
	private static float deadZoneAmount = 0.15f;

	public static float GetXInput(string playerName) {
		string axisName = CheckPlayerNumber (playerName, "Horizontal");
		float playerXInput = ApplyDeadZone(Input.GetAxis (axisName));

		return playerXInput;
	}

	public static float GetZInput(string playerName) {
		string axisName = CheckPlayerNumber (playerName, "Vertical");
		float playerZInput = ApplyDeadZone(Input.GetAxis (axisName));

		return playerZInput;
	}

	public static bool GetButtonInput (string playerName, string buttonName) {
		buttonName = (CheckPlayerNumber(playerName, buttonName));

		bool isButtonPressed = false;
		isButtonPressed = Input.GetButton (buttonName);

		return isButtonPressed;
	}

	public static bool GetButtonDownInput (string playerName, string buttonName) {
		buttonName = (CheckPlayerNumber(playerName, buttonName));

		bool isButtonPressed = false;
		isButtonPressed = Input.GetButtonDown (buttonName);

		return isButtonPressed;
	}

	public static bool GetButtonUpInput (string playerName, string buttonName) {
		buttonName = (CheckPlayerNumber(playerName, buttonName));

		bool isButtonPressed = false;
		isButtonPressed = Input.GetButtonUp (buttonName);

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
