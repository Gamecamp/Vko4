using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AttackMechanics : MonoBehaviour {

	public GameObject parent;
	public GameObject attackerLocation;

	PlayerAttackReceived playerAttackReceived;

	PlayerMovement attackingPlayer;
	PlayerMovement targetPlayer;

	string unarmedLight = "unarmedLight";
	string unarmedHeavy = "unarmedHeavy";
	string baseballBatLight = "baseballBatLight";
	string baseballBatHeavy = "baseballBatHeavy";
	string katanaLight = "katanaLight";
	string katanaHeavy = "katanaHeavy";
	string spearLight = "spearLight";
	string spearHeavy = "spearHeavy";

	string attackType;

	List<PlayerMovement> playersHit = new List<PlayerMovement> ();

	private bool playerWasHit;

	// Use this for initialization
	void Start () {
		
		DetermineAttackType ();
		attackingPlayer = parent.GetComponent<PlayerMovement> ();

		playerWasHit = false;
	}
		


	// Update is called once per frame
	void Update () {

	}
		

	void OnEnable() {
		playerWasHit = false;
		playersHit.Clear();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			targetPlayer = col.gameObject.GetComponent<PlayerMovement> ();

			for (int i = 0; i < playersHit.Count; i++) {
				if (targetPlayer == playersHit [i]) {
					playerWasHit = true;
				}
			}

			if (!playerWasHit) {
				playerAttackReceived = col.gameObject.GetComponent<PlayerAttackReceived> ();
				playerAttackReceived.ReceiveAttack (targetPlayer, attackingPlayer, attackerLocation, attackType);
				playersHit.Add (targetPlayer);
			}
		}

		if (col.gameObject.tag == "Ball") {
			Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();

			float force = 0;
			if (gameObject.name == "UnarmedHitbox") {
				force = 1000;
			} else if (gameObject.name == "BaseballBatLightHitbox") {
				force = 2000;
			} else if (gameObject.name == "KatanaLightHitbox") {
				force = 1500;
			} else if (gameObject.name == "SpearLightHitbox") {
				force = 1200;
			} else if (gameObject.name == "Bullet") {
				force = 500;
			}
		
			rb.AddForce ((col.gameObject.transform.position - attackerLocation.transform.position)*force);

		}
	}

	void DetermineAttackType() {
		if (gameObject.name == "UnarmedHitbox") {
			attackType = unarmedLight;
		} else if (gameObject.name == "UnarmedHeavyHitbox") {
			attackType = unarmedHeavy;
		} else if (gameObject.name == "BaseballBatLightHitbox") {
			attackType = baseballBatLight;
		} else if (gameObject.name == "BaseballBatHeavyHitbox") {
			attackType = baseballBatHeavy;
		} else if (gameObject.name == "KatanaLightHitbox") {
			attackType = katanaLight;
		} else if (gameObject.name == "KatanaHeavyHitbox") {
			attackType = katanaHeavy;
		} else if (gameObject.name == "SpearLightHitbox") {
			attackType = spearLight;
		} else if (gameObject.name == "SpearHeavyHitbox") {
			attackType = spearHeavy;
		}
	}
}
