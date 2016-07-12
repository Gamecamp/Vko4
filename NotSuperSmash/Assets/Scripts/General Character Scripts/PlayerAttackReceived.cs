using UnityEngine;
using System.Collections;

public class PlayerAttackReceived : MonoBehaviour {

	PlayerMovement targetPlayer;
	GameObject attackingPlayer;
	GameObject attackerLocation;

	Vector3 knockbackVector;

	bool attackReceived;
	bool previousAttackDone;

	string attackType;
	string unarmedAttack = "unarmed";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReceiveAttack (PlayerMovement targetPlayer, GameObject attackingPlayer, GameObject attackerLocation, string attackType) {

		attackReceived = true;

		this.attackType = attackType;
	
		this.targetPlayer = targetPlayer;
		this.attackingPlayer = attackingPlayer;
		this.attackerLocation = attackerLocation;

		if (!previousAttackDone) {
			print ("DON'T DELETE if you see me in console. Previous attack wasn't finished, probably problems.");
		} else {
			previousAttackDone = false;
		}

		ReceiveDamage ();
		TurnPlayer ();
		ReceiveKnockback ();
		ReceiveStagger ();


		previousAttackDone = true;
	}


	void ReceiveDamage() {
		
	}

	void TurnPlayer() {
		targetPlayer.LookTowards(attackerLocation.transform.position);
	}

	void ReceiveKnockback() {
		targetPlayer.StartKnockback(gameObject.transform.forward, 20);


	}

	void ReceiveStagger() {
		targetPlayer.StartStagger (1f);
	}
 }
