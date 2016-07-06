using UnityEngine;
using System.Collections;

public static class MyPhysics {
	private static float gravity = 9.81f;
	private static float friction = 0.90f;

	public static void ApplyGravity(GameObject obj) {
		obj.transform.Translate (0, -gravity * Time.deltaTime, 0, Space.World);
	}
	
	public static void ApplyFriction(PlayerMovement obj) {
		obj.SetMoveVector(obj.GetMoveVector() * friction);
	}

	public static void ApplyKnockback(Vector3 fromPos, Vector3 toPos, float force) {
		Vector3 direction = toPos - fromPos;
	}
}
