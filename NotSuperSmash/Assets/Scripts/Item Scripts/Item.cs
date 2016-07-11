using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public int frequency;
	private bool isOnSpawnPoint;

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
}
