using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UnarmedMechanics : MonoBehaviour {

	GameObject parent;
	GameObject attackerLocation;

	PlayerAttackReceived playerAttackReceived;

	PlayerMovement targetPlayer;

	string unarmed = "unarmed";
	string unarmedHeavy = "unarmedHeavy";

	string attackType;

	List<PlayerMovement> playersHit = new List<PlayerMovement> ();

	private bool playerWasHit;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		attackerLocation = GameObject.Find ("PlayerPosition" + parent.name);

		DetermineAttackType ();

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
				playerAttackReceived.ReceiveAttack (targetPlayer, parent, attackerLocation, attackType);
				playersHit.Add (targetPlayer);
			}
		}
	}

	void DetermineAttackType() {
		if (gameObject.name == "UnarmedHitbox" + parent.name) {
			attackType = unarmed;
		} else if (gameObject.name == "UnarmedHeavyHitbox" + parent.name) {
			attackType = unarmedHeavy;
		} 
	}
}
