using UnityEngine;
using System.Collections;

public class BulletMoving : MonoBehaviour {

	private float speed;
	private float killTimer;

	private PlayerAttackReceived targetPlayerReceiving;
	private PlayerMovement targetPlayer;
	private PlayerMovement shootingPlayer;
	private GameObject shooterLocation;
	private string attackType;

	// Use this for initialization
	void Start () {
		speed = 60f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.Self);
		killTimer -= Time.deltaTime;
		if (killTimer < 0) {
			KillBullet ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			targetPlayer = col.gameObject.GetComponent<PlayerMovement> ();
			targetPlayerReceiving = col.gameObject.GetComponent<PlayerAttackReceived> ();
			targetPlayerReceiving.ReceiveAttack (targetPlayer, shootingPlayer, shooterLocation, attackType);
		}
		KillBullet ();
	}

	public void KillBullet() {
		Destroy (gameObject);
	}

	public void SetShooterInfo(PlayerMovement shootingPlayer, GameObject shooterLocation, string gun) {
		this.shooterLocation = shooterLocation;
		this.shootingPlayer = shootingPlayer;
		attackType = gun;

		switch (gun) {
		case "pistolBullet":
			killTimer = 1.5f;
			break;
		case "shotgunBullet":
			killTimer = 0.2f;
			break;
		default:
			killTimer = 2f;
			break;
		}
	}
}
