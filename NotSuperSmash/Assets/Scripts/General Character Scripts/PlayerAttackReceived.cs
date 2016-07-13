using UnityEngine;
using System.Collections;

public class PlayerAttackReceived : MonoBehaviour {

	PlayerMovement targetPlayer;
	GameObject attackingPlayer;
	GameObject attackerLocation;

	Vector3 knockbackVector;

	bool previousAttackDone;

	private float damageAmount;
	private float knockbackAmount;
	private float staggerDuration;

	string attackType;
	string unarmedAttack = "unarmedLight";
	string unarmedHeavyAttack = "unarmedHeavy";
	string baseballBatLight = "baseballBatLight";
	string baseballBatHeavy = "baseballBatHeavy";


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

		DetermineAttackProperties ();
		InterruptActions ();
		TurnPlayer ();

		ReceiveDamage ();
		ReceiveKnockback ();
		ReceiveStagger ();


		previousAttackDone = true;
	}

	void InterruptActions() {
		targetPlayer.InterruptActions ();
	}

	void ReceiveDamage() {
		
	}

	void DetermineAttackProperties() {
		if (attackType == unarmedAttack) {
			knockbackAmount = 20;
			staggerDuration = 1;
		} else if (attackType == unarmedHeavyAttack) {
			knockbackAmount = 30;
			staggerDuration = 1;
		} else if (attackType == baseballBatLight) {
			knockbackAmount = 40;
			staggerDuration = 1;
		} else if (attackType == baseballBatHeavy) {
			knockbackAmount = 50;
			staggerDuration = 1;
		}
	}

	void TurnPlayer() {
		targetPlayer.LookTowards(attackerLocation.transform.position);
	}

	void ReceiveKnockback() {
		targetPlayer.StartKnockback (-gameObject.transform.forward, knockbackAmount);
	}

	void ReceiveStagger() {
		targetPlayer.StartStagger (staggerDuration);
	}
 }
