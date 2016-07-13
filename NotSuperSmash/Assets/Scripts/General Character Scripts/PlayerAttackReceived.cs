using UnityEngine;
using System.Collections;

public class PlayerAttackReceived : MonoBehaviour {

	PlayerMovement targetPlayer;
	GameObject attackingPlayer;
	GameObject attackerLocation;

	Vector3 knockbackVector;

	bool previousAttackDone;

	string attackType;
	string unarmedAttack = "unarmed";
	string unarmedHeavyAttack = "unarmedHeavy";


	// Use this for initialization
	void Start () {
		previousAttackDone = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReceiveAttack (PlayerMovement targetPlayer, GameObject attackingPlayer, GameObject attackerLocation, string attackType) {

		if (!previousAttackDone) {
			print ("DON'T DELETE if you see me in console. Previous attack wasn't finished, probably problems.");
		} else {
			previousAttackDone = false;
		}
			
		this.attackType = attackType;
	
		this.targetPlayer = targetPlayer;
		this.attackingPlayer = attackingPlayer;
		this.attackerLocation = attackerLocation;

		InterruptActions ();

		ReceiveDamage ();
		TurnPlayer ();
		ReceiveKnockback ();
		ReceiveStagger ();


		previousAttackDone = true;
	}

	void InterruptActions() {
		targetPlayer.InterruptActions ();
	}

	void ReceiveDamage() {
		targetPlayer.decreaseHealth (attackingPlayer.GetComponent<PlayerMovement> ().GetAttackDamage ());
	}

	void TurnPlayer() {
		targetPlayer.LookTowards(attackerLocation.transform.position);
	}

	void ReceiveKnockback() {
		if (attackType == unarmedAttack) {
			targetPlayer.StartKnockback (-gameObject.transform.forward, 20);
		} else if (attackType == unarmedHeavyAttack) {
			targetPlayer.StartKnockback (-gameObject.transform.forward, 30);

		}
	}

	void ReceiveStagger() {
		targetPlayer.StartStagger (1f);
	}
 }
