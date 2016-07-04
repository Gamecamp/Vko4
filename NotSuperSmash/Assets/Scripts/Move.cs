using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed;
	public float treshold;
	public float friction;
	
	private Vector3 moveVector;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		
		//moveVector = new Vector3 (moveVector.x - friction, moveVector.y, moveVector.z - friction);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveVector.x += -speed;
			//transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			moveVector.x += speed;
			//transform.Translate (Vector3.right * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			moveVector.z += speed;
			//transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			moveVector.z += -speed;
			//transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
		
		if (Input.GetKeyDown (KeyCode.G)) {
			moveVector.x += 1;
			//transform.Translate (Vector3.back * speed * Time.deltaTime);
		}

		if (moveVector.x < -treshold) {
			moveVector = new Vector3 (moveVector.x * friction, moveVector.y, moveVector.z);
		}

		if (moveVector.x > treshold) {
			moveVector = new Vector3 (moveVector.x * friction, moveVector.y, moveVector.z);
		}

		if (moveVector.z < -treshold) {
			moveVector = new Vector3 (moveVector.x, moveVector.y, moveVector.z * friction);
		}

		if (moveVector.z > treshold) {
			moveVector = new Vector3 (moveVector.x, moveVector.y, moveVector.z * friction);
		}
		
		print (moveVector);

		
		transform.Translate (moveVector * speed * Time.deltaTime, Space.World);
	}
}
