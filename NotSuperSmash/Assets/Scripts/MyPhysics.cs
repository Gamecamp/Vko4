using UnityEngine;
using System.Collections;

public static class MyPhysics {
	
	private static float gravity = 9.81f;
	private static float friction = 0.90f;
	private static float yVelocity = 0;


	public static void ApplyGravity(GameObject obj) {
		yVelocity -= gravity * Time.deltaTime;
		Debug.Log(yVelocity);
		obj.transform.Translate (0, yVelocity, 0, Space.World);
	}
	
	public static void ApplyFriction(PlayerMovement obj) {
		obj.SetMoveVector(obj.GetMoveVector() * friction);
	}

	public static void ApplyKnockback(PlayerMovement obj, Vector3 direction, float force) {
		direction = direction.normalized;
		obj.StartKnockback (direction, force);
	}

	public static void ApplyStagger(PlayerMovement obj, float staggerDuration) {
		obj.StartStagger (staggerDuration);
	}

	public static void SetFriction(float newFriction) {
		friction = newFriction;
	}

	public static void SetGravity(float newGravity) {
		gravity = newGravity;
	}

	public static void ResetFallSpeed() {
		yVelocity = 0;
	}
}
