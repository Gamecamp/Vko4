using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;

	PlayerMovement player1;
	PlayerMovement player2;
	PlayerMovement player3;
	PlayerMovement player4;

	public GameObject t1;
	public GameObject t2;
	public GameObject t3;
	public GameObject t4;

	Text textP1;
	Text textP2;
	Text textP3;
	Text textP4;


	// Use this for initialization
	void Start () {
		if (p1 != null) {
			player1 = p1.GetComponent<PlayerMovement> ();

			textP1 = t1.GetComponent<Text> ();
		}

		if (p2 != null) {
			player2 = p2.GetComponent<PlayerMovement> ();

			textP2 = t2.GetComponent<Text> ();
		}

		if (p3 != null) {
			player3 = p3.GetComponent<PlayerMovement> ();

			textP3 = t3.GetComponent<Text> ();
		}

		if (p4 != null) {
			player4 = p4.GetComponent<PlayerMovement> ();

			textP4 = t4.GetComponent<Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	void UpdateScore() {
		if (p1 != null) {
			textP1.text = "P1 - " + player1.GetCurrentHealth () + "/" + player1.GetMaxHealth () + "\n" + player1.GetCurrentLives() + "/" + player1.GetMaxLives();
		}

		if (p2 != null) {
			textP2.text = "P2 - " + player2.GetCurrentHealth () + "/" + player2.GetMaxHealth () + "\n" + player2.GetCurrentLives() + "/" + player2.GetMaxLives();
		}

		if (p3 != null) {
			textP3.text = "P3 - " + player3.GetCurrentHealth () + "/" + player3.GetMaxHealth () + "\n" + player3.GetCurrentLives() + "/" + player3.GetMaxLives();
		}

		if (p4 != null) {
			textP4.text = "P4 - " + player4.GetCurrentHealth () + "/" + player4.GetMaxHealth () + "\n" + player4.GetCurrentLives() + "/" + player4.GetMaxLives();
		}
	}
}
