using UnityEngine;
using System.Collections;

public static class Physics {
	private static float gravity = 9.81f;
	private static float friction = 0.90f;

	public static void ApplyGravity(GameObject obj) {
		obj.transform.Translate (0, -gravity * Time.deltaTime, 0, Space.World);
	}
	
	public static void ApplyFriction(PlayerMovement obj) {
		obj.moveVector *= friction;
	}
}
