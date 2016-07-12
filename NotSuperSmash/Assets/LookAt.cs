using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public Transform targetPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		transform.LookAt (targetPlayer);
	}
}
