using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bullet;
	public GameObject pistolBulletShootingPoint;
	public GameObject shotgunBulletShootingPoint;
	public GameObject sawedOffBulletShootingPoint;

	private PlayerMovement player;
	private GameObject playerLocation;

	private float timer;
	private int currentClipSize;
	private int pistolClipSize = 16;
	private int shotgunClipSize = 4;
	private int sawedOffClipSize = 2;

	const string Pistol = "pistolBullet";
	const string Shotgun = "shotgunBullet";
	const string SawedOff = "sawedOffBullet";

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

		if (timer <= 0 && currentClipSize > 0) {
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
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Pistol);
			} else if (player.GetIsShotgunEquipped ()) {
				MyPhysics.ApplyKnockback (player, Vector3.back, 15f);
				GameObject temp;
				temp = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation) as GameObject;
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Shotgun);
				GameObject temp1;
				temp1 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 5, 0)) as GameObject;
				temp1.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Shotgun);
				GameObject temp2;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Shotgun);
				GameObject temp3;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -2.5f, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Shotgun);
				GameObject temp4;
				temp2 = Instantiate (bullet, shotgunBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 2.5f, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, Shotgun);
			} else if (player.GetIsSawedOffEquipped ()) {
				MyPhysics.ApplyKnockback (player, Vector3.back, 15f);
				GameObject temp;
				temp = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation) as GameObject;
				temp.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, SawedOff);
				GameObject temp1;
				temp1 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 10, 0)) as GameObject;
				temp1.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, SawedOff);
				GameObject temp2;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -10, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, SawedOff);
				GameObject temp3;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, -5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, SawedOff);
				GameObject temp4;
				temp2 = Instantiate (bullet, sawedOffBulletShootingPoint.transform.position, transform.rotation * Quaternion.Euler (0, 5, 0)) as GameObject;
				temp2.GetComponent<BulletMoving> ().SetShooterInfo (player, playerLocation, SawedOff);
			}

			currentClipSize--;
			timer = bulletCooldown;
		}
	}

	public void Reload(float reloadCooldown) {
		// never called atm
		if (player.GetIsAbleToShoot()) {
			timer = reloadCooldown;
		}
	}

	public void SetCurrentClipSize() {
		if (player.GetIsPistolEquipped ()) {
			currentClipSize = pistolClipSize;
		} else if (player.GetIsShotgunEquipped ()) {
			currentClipSize = shotgunClipSize;
		} else if (player.GetIsSawedOffEquipped ()) {
			currentClipSize = sawedOffClipSize;
		}
	}
}
