using UnityEngine;
using System.Collections;

public class PlayerItemHandling : MonoBehaviour {

	private PlayerMovement player;
	private PlayerAttackManager attackManager;
	private Vector3 hiddenItem;

	public GameObject baseballBatObj;
	public GameObject pistolObj;
	public GameObject shotgunObj;

	const string unarmed = "unarmed";
	const string baseballBat = "baseballBat";
	const string pistol = "pistol";
	const string shotgun = "shotgun";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		attackManager = GetComponent<PlayerAttackManager> ();
		hiddenItem = new Vector3 (0, -10, 0);
		player.SetIsAbleToEquip (true);

		baseballBatObj.SetActive (false);
		pistolObj.SetActive (false);
		shotgunObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.GetIsAbleToEquip () && player.GetIsThrowingInput ()) {
			EquipWeapon (unarmed);
		}
		
	}

	void OnTriggerStay(Collider col) {
		if (player.GetIsAbleToEquip() && player.GetIsEquipInput()) {
			if (col.gameObject.tag == "Item") {
				ReturnSpawnAndIconToRandomPools (col.gameObject);
				EquipWeapon (col.gameObject.name);
			}
		}
	}

	void EquipWeapon(string weapon) {
		SetWeaponStatesToFalse ();

		switch (weapon) {
		case unarmed:
			baseballBatObj.SetActive (false);
			pistolObj.SetActive (false);
			shotgunObj.SetActive (false);
			attackManager.SetActiveWeapon (unarmed);
			player.SetIsAbleToEquip (true);
			break;
		case baseballBat:
			baseballBatObj.SetActive (true);
			attackManager.SetActiveWeapon (baseballBat);
			player.SetIsBaseballBatEquipped (true);
			break;
		case pistol:
			pistolObj.SetActive (true);
			attackManager.SetActiveWeapon (pistol);
			player.SetIsPistolEquipped (true);
			break;
		case shotgun:
			shotgunObj.SetActive (true);
			attackManager.SetActiveWeapon (shotgun);
			player.SetIsShotgunEquipped (true);
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
	}
}
