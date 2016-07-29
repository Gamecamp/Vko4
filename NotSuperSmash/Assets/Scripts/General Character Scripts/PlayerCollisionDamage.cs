using UnityEngine;
using System.Collections;

public class PlayerCollisionDamage : MonoBehaviour {

	public GameObject collisionHurtbox;
	PlayerMovement player;

	float minimumVelocityForDamage;
	float maximumCollisionDamage;
	float collisionDamage;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		minimumVelocityForDamage = 10;
		maximumCollisionDamage = 40;
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Wall" && player.GetIsKnockedBack() && player.GetVelocity() > minimumVelocityForDamage) {
			CountCollisionDamage ();
			ApplyCollisionDamage ();
			StopKnockback ();

		}
	}

	void CountCollisionDamage() {
		if (player.GetVelocity () > maximumCollisionDamage) {
			collisionDamage = maximumCollisionDamage;
		} else {
			collisionDamage = player.GetVelocity ();
		}
	}

	void ApplyCollisionDamage() {
		player.SetCurrentHealth (player.GetCurrentHealth() - collisionDamage);
	}

	void StopKnockback() {
		player.SetIsKnockedBack (false);
	}
}
