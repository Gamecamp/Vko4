using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bullet;
	public GameObject bulletShootingPoint;

	private float timer;

	private PlayerMovement player;
	private GameObject playerLocation;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		playerLocation = GetComponent<PlayerGrapple> ().playerLocation;
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetIsPistolEquipped()) {
			timer -= Time.deltaTime;
		}

		if (timer <= 0) {
			player.SetIsAbleToShoot(true);
		} else {
			player.SetIsAbleToShoot (false);
		}
	}

	public void Shoot(float bulletCooldown) {
		if (player.GetIsAbleToShoot()) {
			GameObject temp;
			temp = Instantiate (bullet, bulletShootingPoint.transform.position, transform.rotation) as GameObject;
			temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation);

			timer = bulletCooldown;
		}
	}

	public void Reload(float reloadCooldown) {
		// never called atm
		if (player.GetIsAbleToShoot()) {
			timer = reloadCooldown;
		}
	}
}
