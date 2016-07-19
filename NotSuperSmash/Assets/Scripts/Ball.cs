using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {


	public float newFriction; 
	public float newGravity;
	public GameObject lights;

	private bool lightsOn;

	private float lightDuration = 5;

	private float durationPassed = 0;

	// Use this for initialization
	void Start () {
		MyPhysics.SetFriction (newFriction);
		MyPhysics.SetGravity (newGravity);

		lights.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (lightsOn) {
			lights.SetActive (true);
			durationPassed = durationPassed + Time.deltaTime;

			if (durationPassed >= lightDuration) {
				lightsOn = false;
				lights.SetActive (false);
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Goal") {
			lightsOn = true;
		}
	}
}
