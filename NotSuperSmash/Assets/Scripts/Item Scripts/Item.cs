using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public int frequency;
	private bool isOnSpawnPoint;
	private GameObject currentSpawnPoint;

	// Use this for initialization
	void Start () {
		SetIsOnSpawnPoint (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GetIsOnSpawnPoint() {
		return isOnSpawnPoint;
	}

	public void SetIsOnSpawnPoint(bool isOnSpawnPoint) {
		this.isOnSpawnPoint = isOnSpawnPoint;
	}

	public GameObject GetCurrentSpawnPoint() {
		return currentSpawnPoint;
	}

	public void SetCurrentSpawnPoint(GameObject currentSpawnPoint) {
		this.currentSpawnPoint = currentSpawnPoint;
	}
}
