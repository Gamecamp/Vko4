using UnityEngine;
using System.Collections;

public class PlayerItemHandling : MonoBehaviour {

	private PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetIsAbleToEquip (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (player.GetIsAbleToEquip() && player.GetIsEquipInput()) {
			if (col.gameObject.tag == "Item") {
				
			}
		}
	}
}
