using UnityEngine;
using System.Collections;

public class UnarmedMechanics : MonoBehaviour {

	GameObject parent;
	GameObject attackerLocation;

	PlayerAttackReceived playerAttackReceived;

	PlayerMovement targetPlayer;

	string unarmed = "unarmed";

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		attackerLocation = GameObject.Find ("PlayerPosition" + parent.name);
		playerAttackReceived = parent.GetComponent<PlayerAttackReceived> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			targetPlayer = col.gameObject.GetComponent<PlayerMovement> ();
			playerAttackReceived.ReceiveAttack (targetPlayer, parent, attackerLocation, unarmed);
		}
	}
}
