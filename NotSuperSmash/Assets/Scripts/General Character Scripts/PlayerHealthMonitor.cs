using UnityEngine;
using System.Collections;

public class PlayerHealthMonitor : MonoBehaviour {

	PlayerMovement player;
	float currentHealth;
	int currentLives;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateStats ();
		CheckAlive ();
	}

	void UpdateStats() {
		currentHealth = player.GetCurrentHealth ();
		currentLives = player.GetCurrentLives ();
	}

	void CheckAlive() {
		if (currentHealth <= 0 && currentLives > 0) {

			currentLives = currentLives - 1;

			player.SetCurrentLives (currentLives);
			if (currentLives == 0) {
				player.Kill ();
			} else {
				player.Respawn ();
			}
		}
	}
}
