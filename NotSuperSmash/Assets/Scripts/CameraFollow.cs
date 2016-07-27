using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	private float highPoint;
	private float lowPoint;
	private float upPoint;
	private float downPoint;
	private float leftPoint;
	private float rightPoint;

	public GameObject[] activePlayers;
	private Camera camera;

	private Vector3 middlePoint;
	private Vector3 cameraPosition;
	private Vector3 distanceBetweenGuys;
	private float distanceFromMiddlePoint;
	private float xDistanceBetweenPlayers;
	private float zDistanceBetweenPlayers;
	private float yDistanceBetweenPlayers;

	private float higherDistanceBetweenPlayers;

	private float minimumYDistance = 20;
	private float maximumYDistance = 90;
	private float yMargin = 10;
	private float threshold = 80;
	private float yDistanceHelper;

	private float thresholdHelper = 0;
	private bool startMinimumLerp;
	private bool startMediumLerp;
	private bool startMaximumLerp;

	private float previousYValue;
	private float previousYValueForMinimum;
	private float previousYValueForLong;

	private float cameraYDistance;
	private float cameraZDistance;
	private float aspectRatio;
	private float tanFov;
	private float tanZ;
	private float margin;

	private float mediumDistanceMultiplier = 0.8f;
	private float maxDistanceMultiplier = 0.65f;

	// Use this for initialization
	void Start () {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan (Mathf.Deg2Rad * Camera.main.fieldOfView * 0.8f);
		tanZ = Mathf.Tan (Mathf.Deg2Rad * 30 * 0.8f);
		highPoint = 0;
		lowPoint = 0;
		leftPoint = 0;
		rightPoint = 0;
		yDistanceHelper = 1;
		camera = GetComponent<Camera> ();
	}

	void Update() {
		DetermineCameraRestrictions ();
		DetermineMiddlePoint ();
		CheckDistanceBetweenPlayers ();
		DetermineCameraDistances ();

		transform.position = cameraPosition;
	}

	void DetermineCameraRestrictions() {

		leftPoint = activePlayers [0].transform.position.x;
		rightPoint = activePlayers [0].transform.position.x;
		lowPoint = activePlayers [0].transform.position.z;
		highPoint = activePlayers[0].transform.position.z;
		upPoint = activePlayers [0].transform.position.y;
		downPoint = activePlayers [0].transform.position.y;


		for (int i = 0; i < activePlayers.Length; i++) {
			if (activePlayers [i].transform.position.x < leftPoint) {
				leftPoint = activePlayers [i].transform.position.x;
			}

			if (activePlayers [i].transform.position.x > rightPoint) {
				rightPoint = activePlayers [i].transform.position.x;
			}

			if (activePlayers [i].transform.position.z < downPoint) {
				downPoint = activePlayers [i].transform.position.z;
			}

			if (activePlayers [i].transform.position.z > upPoint) {
				upPoint = activePlayers [i].transform.position.z;
			}

			if (activePlayers [i].transform.position.y < lowPoint) {
				lowPoint = activePlayers [i].transform.position.y;
			}

			if (activePlayers [i].transform.position.y > highPoint) {
				highPoint = activePlayers [i].transform.position.y;
			}
		}
	}

	void DetermineMiddlePoint() {
		middlePoint = new Vector3 ((rightPoint + leftPoint) / 2, (highPoint + lowPoint) / 2 , (upPoint + downPoint) / 2); 
	}

	void CheckDistanceBetweenPlayers() {
		xDistanceBetweenPlayers = upPoint - downPoint;
		zDistanceBetweenPlayers = rightPoint - leftPoint;
		yDistanceBetweenPlayers = highPoint - lowPoint;

		if (yDistanceBetweenPlayers > 15) {

			if (yDistanceHelper < 100) {
				yDistanceHelper = yDistanceHelper + 2f;
			}
		} else {
			
			if (yDistanceHelper > 1) {
				yDistanceHelper = yDistanceHelper - 2f;
			}
		}

		highPoint = highPoint + yDistanceHelper;


		distanceBetweenGuys = new Vector3 (rightPoint, highPoint, upPoint) - new Vector3 (leftPoint, lowPoint, downPoint);
	}

	void DetermineCameraDistances() {

		cameraYDistance = (distanceBetweenGuys.magnitude / 2.0f) / tanFov;

		cameraPosition = middlePoint;



		if (cameraYDistance * mediumDistanceMultiplier < minimumYDistance) {
			if (!startMinimumLerp) {
				startMinimumLerp = true;
				startMediumLerp = false;
				startMaximumLerp = false;
				thresholdHelper = 0;
				print ("close");
			}

			thresholdHelper += Time.deltaTime;
			cameraPosition.y = Mathf.Lerp(previousYValueForMinimum, minimumYDistance, thresholdHelper);;
			previousYValue = minimumYDistance;
		} else if (cameraYDistance > threshold) {
			if (!startMaximumLerp) {
				startMinimumLerp = false;
				startMediumLerp = false;
				startMaximumLerp = true;
				thresholdHelper = 0;
				print ("long");
			}
			thresholdHelper += Time.deltaTime;
			cameraPosition.y = Mathf.Lerp(previousYValueForLong, cameraYDistance * maxDistanceMultiplier, thresholdHelper);
			previousYValue = cameraPosition.y;
		} else if (cameraYDistance * maxDistanceMultiplier > maximumYDistance) {
			cameraPosition.y = maximumYDistance;
			print ("max");

		} else {
			if (!startMediumLerp) {
				startMinimumLerp = false;
				startMediumLerp = true;
				startMaximumLerp = false;
				thresholdHelper = 0;
				print ("medium");
			}
			thresholdHelper += Time.deltaTime;
			cameraPosition.y = Mathf.Lerp (previousYValue, cameraYDistance * mediumDistanceMultiplier, thresholdHelper);
			previousYValueForMinimum = cameraPosition.y;
			previousYValueForLong = cameraPosition.y;
		}
			


		cameraZDistance = (cameraPosition.y) / tanZ;

		cameraPosition.z = cameraPosition.z - cameraZDistance + yMargin;



	}
	
}
