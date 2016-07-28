using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	private int player1Active;
	private int player2Active;
	private int player3Active;
	private int player4Active;

	void Awake() {
		player1Active = PlayerPrefs.GetInt ("Player1");
		player2Active = PlayerPrefs.GetInt ("Player2");
		player3Active = PlayerPrefs.GetInt ("Player3");
		player4Active = PlayerPrefs.GetInt ("Player4");

		if (player1Active == 0) {
			player1.SetActive (false);
		}

		if (player2Active == 0) {
			player2.SetActive (false);
		}

		if (player3Active == 0) {
			player3.SetActive (false);
		}

		if (player4Active == 0) {
			player4.SetActive (false);
		}
	}
}
