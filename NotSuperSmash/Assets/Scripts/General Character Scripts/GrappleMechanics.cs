using UnityEngine;
using System.Collections;

public class GrappleMechanics : MonoBehaviour {

	GameObject parent;

	PlayerGrapple playerGrappling;

	PlayerMovement targetPlayer;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		playerGrappling = parent.GetComponent<PlayerGrapple> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player" && !playerGrappling.GetGrappleIsHappening()) {
			targetPlayer = col.gameObject.GetComponent<PlayerMovement> ();
			playerGrappling.BeginGrappling (targetPlayer);
			gameObject.SetActive (false);

		}
	}


}
