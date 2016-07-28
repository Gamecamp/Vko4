using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelStart : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	public GameObject player1HealthBar;
	public GameObject player2HealthBar;
	public GameObject player3HealthBar;
	public GameObject player4HealthBar;

	public GameObject congratulationText;

	public GameObject camera;

	private CameraFollow cameraFollow;

	public List<GameObject> activePlayers;

	private int player1Active;
	private int player2Active;
	private int player3Active;
	private int player4Active;

	void Awake() {
		player1Active = PlayerPrefs.GetInt ("Player1");
		player2Active = PlayerPrefs.GetInt ("Player2");
		player3Active = PlayerPrefs.GetInt ("Player3");
		player4Active = PlayerPrefs.GetInt ("Player4");

		congratulationText.SetActive (false);
		cameraFollow = camera.GetComponent<CameraFollow> ();

		if (player1Active == 0) {
			player1.SetActive (false);
			player1HealthBar.SetActive (false);
		} else {
			activePlayers.Add (player1);
		}

		if (player2Active == 0) {
			player2.SetActive (false);
			player2HealthBar.SetActive (false);
		} else {
			activePlayers.Add (player2);
		}

		if (player3Active == 0) {
			player3.SetActive (false);
			player3HealthBar.SetActive (false);
		} else {
			activePlayers.Add (player3);
		}

		if (player4Active == 0) {
			player4.SetActive (false);
			player4HealthBar.SetActive (false);
		} else {
			activePlayers.Add (player4);
		}

		cameraFollow.SetActivePlayers (activePlayers);
	}
}
