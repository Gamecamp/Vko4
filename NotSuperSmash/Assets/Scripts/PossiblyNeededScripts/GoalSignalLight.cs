using UnityEngine;
using System.Collections;

public class GoalSignalLight : MonoBehaviour {

	public GameObject spotlights;
	public float rotateSpeed;

	private float startTime;
	public float lightTime;

	// Use this for initialization
	void Start () {
		//LightSwitch (false);
	}
	
	// Update is called once per frame
	void Update () {
		spotlights.transform.Rotate (new Vector3(0, rotateSpeed, 0));
//		if (Time.time - startTime > lightTime) {
//			LightSwitch (false);
//		}
	}

//	void LightSwitch(bool lightsOn) {
//		spotlights.SetActive (lightsOn);
//	}
//
//	public void GoalScored() {
//		LightSwitch (true);
//		startTime = Time.time;
//	}
}
