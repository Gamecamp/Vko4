using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public Animator pressAnyButtonAnimator;
	public Animator playerSelectionAnimator;

	public GameObject player1JoinText;
	public GameObject player2JoinText;
	public GameObject player3JoinText;
	public GameObject player4JoinText;
	public GameObject player1OkText;
	public GameObject player2OkText;
	public GameObject player3OkText;
	public GameObject player4OkText;

	private bool startUpMode;
	private bool playerSelectionMode;

	private int playerAmount;
	private bool player1Active;
	private bool player2Active;
	private bool player3Active;
	private bool player4Active;

	// Use this for initialization
	void Start () {
		startUpMode = true;
		playerSelectionMode = false;
		playerAmount = 0;
		player1Active = false;
		player2Active = false;
		player3Active = false;
		player4Active = false;
		player1OkText.SetActive(false);
		player2OkText.SetActive(false);
		player3OkText.SetActive(false);
		player4OkText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (startUpMode && Input.anyKeyDown) {
			StartPlayerSelectionMode ();
		}

		if (playerSelectionMode) {
			if (Input.GetButtonDown ("P1AButton") && !player1Active) {
				PlayerActivation ("Player1", true);
				playerAmount++;
				player1Active = true;
			} else if (Input.GetButtonDown ("P2AButton") && !player2Active) {
				PlayerActivation ("Player2", true);
				playerAmount++;
				player2Active = true;
			} else if (Input.GetButtonDown ("P3AButton") && !player3Active) {
				PlayerActivation ("Player3", true);
				playerAmount++;
				player3Active = true;
			} else if (Input.GetButtonDown ("P4AButton") && !player4Active) {
				PlayerActivation ("Player4", true);
				playerAmount++;
				player4Active = true;
			}
			if (Input.GetButtonDown ("P1BButton") && player1Active) {
				PlayerActivation ("Player1", false);
				playerAmount--;
				player1Active = false;
			} else if (Input.GetButtonDown ("P2BButton") && player2Active) {
				PlayerActivation ("Player2", false);
				playerAmount--;
				player2Active = false;
			} else if (Input.GetButtonDown ("P3BButton") && player3Active) {
				PlayerActivation ("Player3", false);
				playerAmount--;
				player3Active = false;
			} else if (Input.GetButtonDown ("P4BButton") && player4Active) {
				PlayerActivation ("Player4", false);
				playerAmount--;
				player4Active = false;
			}

			if (playerAmount >= 1 && playerAmount <= 4 && Input.GetButtonDown ("Submit")) {
				StartGame ();
			}
		}
	}

	void StartPlayerSelectionMode() {
		startUpMode = false;
		playerSelectionMode = true;
		pressAnyButtonAnimator.SetBool ("buttonPressed", true);
		playerSelectionAnimator.SetBool ("slideIn", true);
	}

	void StartGame() {
		SaveActivePlayers ();
		playerSelectionAnimator.SetBool ("slideIn", false);
		SceneManager.LoadScene ("Level1");
	}

	void SaveActivePlayers() {
		int active;

		if (player1Active) {
			active = 1;
		} else {
			active = 0;
		}
		PlayerPrefs.SetInt ("Player1", active);

		if (player2Active) {
			active = 1;
		} else {
			active = 0;
		}
		PlayerPrefs.SetInt ("Player2", active);

		if (player3Active) {
			active = 1;
		} else {
			active = 0;
		}
		PlayerPrefs.SetInt ("Player3", active);

		if (player4Active) {
			active = 1;
		} else {
			active = 0;
		}
		PlayerPrefs.SetInt ("Player4", active);
	}

	void PlayerActivation(string player, bool active) {
		switch (player) {
		case "Player1":
			if (active) {
				player1JoinText.SetActive (false);
				player1OkText.SetActive (true);
			} else {
				player1JoinText.SetActive (true);
				player1OkText.SetActive (false);
			}
			break;
		case "Player2":
			if (active) {
				player2JoinText.SetActive (false);
				player2OkText.SetActive (true);
			} else {
				player2JoinText.SetActive (true);
				player2OkText.SetActive (false);
			}
			break;
		case "Player3":
			if (active) {
				player3JoinText.SetActive (false);
				player3OkText.SetActive (true);
			} else {
				player3JoinText.SetActive (true);
				player3OkText.SetActive (false);
			}
			break;
		case "Player4":
			if (active) {
				player4JoinText.SetActive (false);
				player4OkText.SetActive (true);
			} else {
				player4JoinText.SetActive (true);
				player4OkText.SetActive (false);
			}
			break;
		}
	}
}
