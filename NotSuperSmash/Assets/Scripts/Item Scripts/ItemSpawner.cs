using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour {

	public List<GameObject> spawnPoints;

	private GameObject items;
	public List<GameObject> itemList;
	private int itemsWeight;

	public float itemSpawnProbability;
	public int maxItemsOnSpawnPoints;
	private int itemsOnSpawnPoints;

	// Use this for initialization
	void Start () {
		items = GameObject.Find ("Items");
		itemsWeight = 0;

		foreach (Transform spawner in transform) {
			spawnPoints.Add (spawner.gameObject);
		}

		foreach (Transform item in items.transform) {
			itemList.Add (item.gameObject);
			itemsWeight += item.GetComponent<Item> ().frequency;
		}

		itemsOnSpawnPoints = 0;
	}

	// Update is called once per frame
	void Update () {
		RandomizeItemSpawn ();
	}

	void RandomizeItemSpawn() {
		if (itemsOnSpawnPoints < maxItemsOnSpawnPoints) {
			if (Random.value < itemSpawnProbability) {
				int spawnIndex = (int) Random.Range (0, spawnPoints.Count);
				if (spawnIndex == spawnPoints.Count) {
					spawnIndex--;
				}

				int weaponIndex = itemList.Count - 1;	

				for (float rnd = Random.Range (0, itemsWeight); rnd > 0; rnd -= itemList[weaponIndex].GetComponent<Item>().frequency) {
					if (weaponIndex > 0) {
						weaponIndex--;
					}
				}

				itemList [weaponIndex].transform.position = spawnPoints [spawnIndex].transform.position;
				itemList [weaponIndex].GetComponent<Item> ().SetIsOnSpawnPoint (true);
				itemList [weaponIndex].GetComponent<Item> ().SetCurrentSpawnPoint (spawnPoints[spawnIndex]);

				itemsWeight -= itemList[weaponIndex].GetComponent<Item> ().frequency;
				itemsOnSpawnPoints++;
				spawnPoints.RemoveAt (spawnIndex);
				itemList.RemoveAt (weaponIndex);
			}
		}
	}

	public void AddSpawnToRandomPool(GameObject spawn) {
		spawnPoints.Add (spawn);
	}

	public void AddItemToRandomPool(GameObject item) {
		itemList.Add (item);
		itemsWeight += item.GetComponent<Item> ().frequency;
		itemsOnSpawnPoints--;
	}
}
