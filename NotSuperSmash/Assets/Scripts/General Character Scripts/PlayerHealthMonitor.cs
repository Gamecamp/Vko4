using UnityEngine;
using System.Collections;

public class PlayerHealthMonitor : MonoBehaviour {

	PlayerMovement player;
	float currentHealth;
	float currentHealthPercentage;
	int currentLives;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetDeathHandled (false);
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
		if (currentHealth <= 0 && currentLives > 0 && !player.GetDeathHandled()) {

			currentLives = currentLives - 1;

			player.SetCurrentLives (currentLives);
			if (currentLives == 0) {
				player.Kill ();
				player.SetDeathHandled(true);
			} else {
				player.Respawn ();
				player.SetDeathHandled(true);
			}
		}
	}
}
