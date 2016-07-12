using UnityEngine;
using System.Collections;

public class PlayerItemHandling : MonoBehaviour {

	private PlayerMovement player;
	private Vector3 hiddenItem;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		hiddenItem = new Vector3 (0, -10, 0);
		player.SetIsAbleToEquip (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col) {
		if (player.GetIsAbleToEquip() && player.GetIsEquipInput()) {
			if (col.gameObject.tag == "Item") {
				col.gameObject.transform.position = hiddenItem;
				GameObject.Find ("ItemSpawner").GetComponent<ItemSpawner> ().AddItemToRandomPool (col.gameObject);
				col.gameObject.GetComponent<Item> ().SetIsOnSpawnPoint (false);
				GameObject.Find ("ItemSpawner").GetComponent<ItemSpawner> ().AddSpawnToRandomPool (col.gameObject.GetComponent<Item> ().GetCurrentSpawnPoint ());
			}
		}
	}
}
