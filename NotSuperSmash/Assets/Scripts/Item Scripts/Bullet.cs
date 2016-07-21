using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bullet;
	public GameObject pistolBulletShootingPoint;
	public GameObject shotgunBulletShootingPoint;
	public GameObject sawedOffBulletShootingPoint;

	private float timer;

	private PlayerMovement player;
	private GameObject playerLocation;

	const string pistol = "pistolBullet";
	const string shotgun = "shotgunBullet";
	const string sawedOff = "sawedOffBullet";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		playerLocation = GetComponent<PlayerGrapple> ().playerLocation;
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.GetIsPistolEquipped() || player.GetIsShotgunEquipped() || player.GetIsSawedOffEquipped()) && timer > 0) {
			timer -= Time.deltaTime;
		}

		if (timer <= 0) {
			player.SetIsAbleToShoot (true);
		} else {
			player.SetIsAbleToShoot (false);
		}
	}

	public void Shoot(float bulletCooldown) {
		if (player.GetIsAbleToShoot()) {
			if (player.GetIsPistolEquipped ()) {
				GameObject temp;
				temp = Instantiate (bullet, pistolBulletShootingPoint.transform.position, transform.rotation) as GameObject;
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, pistol);
			} else if (player.GetIsShotgunEquipped ()) {
				GameObject temp;
				temp = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation) as GameObject;
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, shotgun);
				GameObject temp1;
				temp1 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 5, 0)) as GameObject;
				temp1.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, shotgun);
				GameObject temp2;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, shotgun);
				GameObject temp3;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -2.5f, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, shotgun);
				GameObject temp4;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 2.5f, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, shotgun);
			} else if (player.GetIsSawedOffEquipped ()) {
				GameObject temp;
				temp = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation) as GameObject;
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, sawedOff);
				GameObject temp1;
				temp1 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 10, 0)) as GameObject;
				temp1.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, sawedOff);
				GameObject temp2;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -10, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, sawedOff);
				GameObject temp3;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, sawedOff);
				GameObject temp4;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, sawedOff);
			}

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
