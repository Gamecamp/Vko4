﻿using UnityEngine;
using System.Collections;

public class FollowHand : MonoBehaviour {
	public Transform handTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = handTransform.position;
	}
}
