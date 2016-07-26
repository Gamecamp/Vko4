using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	private float highPoint;
	private float lowPoint;
	private float leftPoint;
	private float rightPoint;

	public GameObject[] activePlayers;
	private Camera camera;

	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float xDistanceBetweenPlayers;
	private float zDistanceBetweenPlayers;
	private float higherDistanceBetweenPlayers;
	private float minimumZDistance = 30;
	private float minimumYDistance = 20;

	private float cameraYDistance;
	private float aspectRatio;
	private float tanFov;
	private float margin;

	private float zDistanceMultiplier = 0.65f;
	private float yDistanceMultiplier = 0.5f;

	// Use this for initialization
	void Start () {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan (Mathf.Deg2Rad * Camera.main.fieldOfView * 0.5f);
		highPoint = 0;
		lowPoint = 0;
		leftPoint = 0;
		rightPoint = 0;
		camera = GetComponent<Camera> ();
	}

	void Update() {
		DetermineCameraRestrictions ();
		DetermineMiddlePoint ();
		CheckDistanceBetweenPlayers ();

		cameraYDistance = (higherDistanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
//
//		float zDistance = Mathf.Abs (vectorBetweenPlayers.z);
//		distanceMultiplier = 1 + zDistance / 90;

		if (higherDistanceBetweenPlayers * zDistanceMultiplier < minimumZDistance) {
			middlePoint.z = middlePoint.z - minimumZDistance;
		} else {
			middlePoint.z = middlePoint.z - higherDistanceBetweenPlayers * zDistanceMultiplier;
		}

		if (cameraYDistance * yDistanceMultiplier  < minimumYDistance) {
			middlePoint.y = minimumYDistance;
		} else {
			middlePoint.y = cameraYDistance * yDistanceMultiplier;
		}


		transform.position = middlePoint;
	}

	void DetermineCameraRestrictions() {

		leftPoint = activePlayers [0].transform.position.x;
		rightPoint = activePlayers [0].transform.position.x;
		lowPoint = activePlayers [0].transform.position.z;
		highPoint = activePlayers[0].transform.position.z;


		for (int i = 0; i < activePlayers.Length; i++) {
			if (activePlayers [i].transform.position.x < leftPoint) {
				leftPoint = activePlayers [i].transform.position.x;
				print (leftPoint);
			}

			if (activePlayers [i].transform.position.x > rightPoint) {
				rightPoint = activePlayers [i].transform.position.x;
				print (rightPoint);

			}

			if (activePlayers [i].transform.position.z < lowPoint) {
				lowPoint = activePlayers [i].transform.position.z;
				print (lowPoint);

			}

			if (activePlayers [i].transform.position.z > highPoint) {
				highPoint = activePlayers [i].transform.position.z;
				print (highPoint);

			}
		}
	}

	void DetermineMiddlePoint() {
		middlePoint = new Vector3 ((rightPoint + leftPoint) / 2, 0, (highPoint + lowPoint)/2); 
	}

	void CheckDistanceBetweenPlayers() {
		xDistanceBetweenPlayers = highPoint - lowPoint;
		zDistanceBetweenPlayers = rightPoint - leftPoint;

		xDistanceBetweenPlayers = xDistanceBetweenPlayers / aspectRatio;
		zDistanceBetweenPlayers = zDistanceBetweenPlayers * aspectRatio;

		if (xDistanceBetweenPlayers > zDistanceBetweenPlayers) {
			higherDistanceBetweenPlayers = xDistanceBetweenPlayers;
		} else {
			higherDistanceBetweenPlayers = zDistanceBetweenPlayers;
		}
	}
	
}
