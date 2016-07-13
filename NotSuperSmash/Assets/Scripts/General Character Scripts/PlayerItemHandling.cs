using UnityEngine;
using System.Collections;

public class PlayerItemHandling : MonoBehaviour {

	private PlayerMovement player;
	private Vector3 hiddenItem;

	private GameObject baseballBatObj;

	const string baseballBat = "BaseballBat";

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		hiddenItem = new Vector3 (0, -10, 0);
		player.SetIsAbleToEquip (true);

		baseballBatObj = GameObject.Find ("BaseballBat" + gameObject.name);
		baseballBatObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
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
		switch (weapon) {
		case baseballBat:
			baseballBatObj.SetActive (true);
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
}
