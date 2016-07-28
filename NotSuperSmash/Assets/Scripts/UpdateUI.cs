using UnityEngine;
using System.Collections;

public class UpdateUI : MonoBehaviour {

	public GameObject player1RemainingHP;
	public GameObject player2RemainingHP;
	public GameObject player3RemainingHP;
	public GameObject player4RemainingHP;

	public GameObject[] player1Lives;
	public GameObject[] player2Lives;
	public GameObject[] player3Lives;
	public GameObject[] player4Lives;


	private RectTransform player1RemainingHPRect;
	private RectTransform player2RemainingHPRect;
	private RectTransform player3RemainingHPRect;
	private RectTransform player4RemainingHPRect;

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	private PlayerMovement player1HP;
	private PlayerMovement player2HP;
	private PlayerMovement player3HP;
	private PlayerMovement player4HP;

	private float originalSize = 200;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("Player1") == 1) {
			player1HP = player1.GetComponent<PlayerMovement> ();
			player1RemainingHPRect = player1RemainingHP.GetComponent<RectTransform> ();
		}

		if (PlayerPrefs.GetInt("Player2") == 1) {
			player2HP = player2.GetComponent<PlayerMovement> ();
			player2RemainingHPRect = player2RemainingHP.GetComponent<RectTransform> ();

		}

		if (PlayerPrefs.GetInt("Player3") == 1) {
			player3HP = player3.GetComponent<PlayerMovement> ();
			player3RemainingHPRect = player3RemainingHP.GetComponent<RectTransform> ();

		}

		if (PlayerPrefs.GetInt("Player4") == 1) {
			player4HP = player4.GetComponent<PlayerMovement> ();
			player4RemainingHPRect = player4RemainingHP.GetComponent<RectTransform> ();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (PlayerPrefs.GetInt("Player1") == 1) {
			player1RemainingHPRect.sizeDelta = new Vector2 (originalSize * (player1HP.GetCurrentHealth () / player1HP.GetMaxHealth ()), player1RemainingHPRect.sizeDelta.y);
		}

		if (PlayerPrefs.GetInt("Player2") == 1) {
			player2RemainingHPRect.sizeDelta = new Vector2 (originalSize * (player2HP.GetCurrentHealth () / player2HP.GetMaxHealth ()), player2RemainingHPRect.sizeDelta.y);

		}

		if (PlayerPrefs.GetInt("Player3") == 1) {
			player3RemainingHPRect.sizeDelta = new Vector2 (originalSize * (player3HP.GetCurrentHealth () / player3HP.GetMaxHealth ()), player3RemainingHPRect.sizeDelta.y);

		}

		if (PlayerPrefs.GetInt("Player4") == 1) {
			player4RemainingHPRect.sizeDelta = new Vector2 (originalSize * (player4HP.GetCurrentHealth () / player4HP.GetMaxHealth ()), player4RemainingHPRect.sizeDelta.y);

		}
	}

	public void RemoveLife(string player) {
		switch (player) {
		case "Player1":
			player1Lives [(int)player1HP.GetCurrentLives()].SetActive (false);
			break;
		case "Player2":
			player2Lives [(int)player2HP.GetCurrentLives()].SetActive (false);
			break;
		case "Player3":
			player3Lives [(int)player3HP.GetCurrentLives()].SetActive (false);
			break;
		case "Player4":
			player4Lives [(int)player4HP.GetCurrentLives()].SetActive (false);
			break;
		}
	}
}
