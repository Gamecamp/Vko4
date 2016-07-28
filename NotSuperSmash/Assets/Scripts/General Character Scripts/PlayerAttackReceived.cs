using UnityEngine;
using System.Collections;

public class PlayerAttackReceived : MonoBehaviour {

	PlayerMovement targetPlayer;
	PlayerMovement attackingPlayer;
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
	string pistolBullet = "pistolBullet";
	string shotgunBullet = "shotgunBullet";
	string sawedOffBullet = "sawedOffBullet";
	string katanaLight = "katanaLight";
	string katanaHeavy = "katanaHeavy";
	string spearLight = "spearLight";
	string spearHeavy = "spearHeavy";


	// Use this for initialization
	void Start () {
		previousAttackDone = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReceiveAttack (PlayerMovement targetPlayer, PlayerMovement attackingPlayer, GameObject attackerLocation, string attackType) {

		if (!previousAttackDone) {
			print ("DON'T DELETE if you see me in console. Previous attack wasn't finished, probably problems.");
		} else {
			previousAttackDone = false;
		}
			
		this.attackType = attackType;
	
		this.targetPlayer = targetPlayer;
		this.attackingPlayer = attackingPlayer;
		this.attackerLocation = attackerLocation;

		if (!targetPlayer.GetIsGuarding ()) {
			DetermineAttackProperties ();
			InterruptActions ();
			TurnPlayer ();

			ReceiveDamage ();
			ReceiveKnockback ();
			ReceiveStagger ();
		}
			
		previousAttackDone = true;
	}

	void InterruptActions() {
		targetPlayer.InterruptActions ();
	}

	void ReceiveDamage() {
		//targetPlayer.decreaseHealth (attackingPlayer.GetComponent<PlayerMovement> ().GetAttackDamage ());
		targetPlayer.decreaseHealth (100);

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
		} else if (attackType == pistolBullet) {
			knockbackAmount = 15;
			staggerDuration = 0.2f;
		} else if (attackType == shotgunBullet) {
			knockbackAmount = 25;
			staggerDuration = 1.5f;
		} else if (attackType == sawedOffBullet) {
			knockbackAmount = 35;
			staggerDuration = 2f;
		} else if (attackType == katanaLight) {
			knockbackAmount = 20;
			staggerDuration = 1;
		} else if (attackType == katanaHeavy) {
			knockbackAmount = 40;
			staggerDuration = 1;
		} else if (attackType == spearLight) {
			knockbackAmount = 20;
			staggerDuration = 1;
		} else if (attackType == spearHeavy) {
			knockbackAmount = 25;
			staggerDuration = 2;
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
