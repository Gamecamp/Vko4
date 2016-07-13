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
	}

	void DetermineAttackType() {
		if (gameObject.name == "UnarmedHitbox" + parent.name) {
			attackType = unarmedLight;
		} else if (gameObject.name == "UnarmedHeavyHitbox" + parent.name) {
			attackType = unarmedHeavy;
		} else if (gameObject.name == "BaseballBatLightHitbox" + parent.name) {
			attackType = baseballBatLight;
		} else if (gameObject.name == "BaseballBatHeavyHitbox" + parent.name) {
			attackType = baseballBatHeavy;
		}
	}
}
