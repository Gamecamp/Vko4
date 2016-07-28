using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthMonitor : MonoBehaviour {

	public GameObject camera;
	private CameraFollow camFollow;

	PlayerMovement player;
	float currentHealth;
	float currentHealthPercentage;
	int currentLives;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMovement> ();
		player.SetDeathHandled (false);
		camFollow = camera.GetComponent<CameraFollow> ();
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
				player.SetDeathHandled (true);
				List<GameObject> activePlayers = camFollow.GetActivePlayers();
				for (int i = 0; activePlayers.Count > i; i++) {
					if (activePlayers [i].name == gameObject.name) {
						activePlayers.RemoveAt (i);
						camFollow.SetActivePlayers(activePlayers);
					}
				}
			} else {
				player.Respawn ();
				player.SetDeathHandled (true);
			}
		}
	}
}
