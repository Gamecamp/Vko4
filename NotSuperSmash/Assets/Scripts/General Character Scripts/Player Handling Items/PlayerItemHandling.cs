using UnityEngine;
using System.Collections;

public class PlayerItemHandling : MonoBehaviour {

	private PlayerMovement player;
	private PlayerAttackManager attackManager;
	private Vector3 hiddenItem;

	private bool weaponThrown;

	private string currentWeapon;
	private GameObject currentThrowable;
	private float throwForce = 50f;
	public GameObject throwingPoint;

	public GameObject baseballBatObj;
	public GameObject pistolObj;
	public GameObject shotgunObj;
	public GameObject katanaObj;
	public GameObject sawedOffObj;
	public GameObject spearObj;

	public GameObject throwBaseballBat;
	public GameObject throwPistol;
	public GameObject throwShotgun;
	public GameObject throwKatana;
	public GameObject throwSawedOff;
	public GameObject throwSpear;

	const string unarmed = "unarmed";
	const string baseballBat = "baseballBat";
	const string pistol = "pistol";
	const string shotgun = "shotgun";
	const string katana = "katana";
	const string sawedOff = "sawedOff";
	const string spear = "spear";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		attackManager = GetComponent<PlayerAttackManager> ();
		hiddenItem = new Vector3 (0, -10, 0);
		player.SetIsAbleToEquip (true);
		weaponThrown = false;

		baseballBatObj.SetActive (false);
		pistolObj.SetActive (false);
		shotgunObj.SetActive (false);
		katanaObj.SetActive (false);
		sawedOffObj.SetActive (false);
		spearObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.GetIsAbleToEquip () && player.GetIsThrowingInput () && player.GetCanInputActions() && !weaponThrown) {
			GameObject temp;
			temp = Instantiate (currentThrowable, throwingPoint.transform.position, transform.rotation) as GameObject;
			Rigidbody rb;
			rb = temp.GetComponent<Rigidbody> ();
			rb.AddForce ((throwingPoint.transform.position - transform.position).normalized * throwForce);
			rb.angularVelocity = new Vector3 (0, 100, 0);

			weaponThrown = true;

			EquipWeapon (unarmed);

			StartCoroutine (AbleToEquipAgain ());
		}
		
	}

	IEnumerator AbleToEquipAgain() {
		yield return new WaitForSeconds (0.3f);
		player.SetIsAbleToEquip (true);
		weaponThrown = false;

	}

	void OnTriggerStay(Collider col) {
		if (player.GetIsAbleToEquip() && player.GetIsEquipInput() && player.GetCanInputActions()) {
			if (col.gameObject.tag == "Item") {
				ReturnSpawnAndIconToRandomPools (col.gameObject);
				EquipWeapon (col.gameObject.name);
			}
		}
	}

	void EquipWeapon(string weapon) {
		SetWeaponStatesToFalse ();

		currentWeapon = weapon;

		switch (weapon) {
		case unarmed:
			baseballBatObj.SetActive (false);
			pistolObj.SetActive (false);
			shotgunObj.SetActive (false);
			katanaObj.SetActive (false);
			sawedOffObj.SetActive (false);
			spearObj.SetActive (false);
			attackManager.SetActiveWeapon (unarmed);
			break;
		case baseballBat:
			baseballBatObj.SetActive (true);
			attackManager.SetActiveWeapon (baseballBat);
			player.SetIsBaseballBatEquipped (true);
			currentThrowable = throwBaseballBat;
			break;
		case pistol:
			pistolObj.SetActive (true);
			attackManager.SetActiveWeapon (pistol);
			player.SetIsPistolEquipped (true);
			GetComponent<Bullet> ().SetCurrentClipSize ();
			currentThrowable = throwPistol;
			break;
		case shotgun:
			shotgunObj.SetActive (true);
			attackManager.SetActiveWeapon (shotgun);
			player.SetIsShotgunEquipped (true);
			GetComponent<Bullet> ().SetCurrentClipSize ();
			currentThrowable = throwShotgun;
			break;
		case katana:
			katanaObj.SetActive (true);
			attackManager.SetActiveWeapon (katana);
			player.SetIsKatanaEquipped (true);
			currentThrowable = throwKatana;
			break;
		case sawedOff:
			sawedOffObj.SetActive (true);
			attackManager.SetActiveWeapon (sawedOff);
			player.SetIsSawedOffEquipped (true);
			GetComponent<Bullet> ().SetCurrentClipSize ();
			currentThrowable = throwSawedOff;
			break;
		case spear:
			spearObj.SetActive (true);
			attackManager.SetActiveWeapon (spear);
			player.SetIsSpearEquipped (true);
			currentThrowable = throwSpear;
			break;
		}
	}

	void ReturnSpawnAndIconToRandomPools(GameObject icon) {
		icon.transform.position = hiddenItem;
		GameObject.Find ("ItemSpawner").GetComponent<ItemSpawner> ().AddItemToRandomPool (icon);
		icon.GetComponent<Item> ().SetIsOnSpawnPoint (false);
		GameObject.Find ("ItemSpawner").GetComponent<ItemSpawner> ().AddSpawnToRandomPool (
			icon.GetComponent<Item> ().GetCurrentSpawnPoint ());
	}

	void SetWeaponStatesToFalse() {
		player.SetIsAbleToEquip (false);
		player.SetIsBaseballBatEquipped (false);
		player.SetIsPistolEquipped (false);
		player.SetIsShotgunEquipped (false);
		player.SetIsKatanaEquipped (false);
		player.SetIsSawedOffEquipped (false);
		player.SetIsSpearEquipped (false);
	}
}
