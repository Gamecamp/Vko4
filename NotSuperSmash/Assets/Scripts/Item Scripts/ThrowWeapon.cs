using UnityEngine;
using System.Collections;

public class ThrowWeapon : MonoBehaviour {

	private float killTimer = 2f;
	private float endTime;
	private float throwDamage = 20f;
	private bool itemCanHurt;
	private float blinkTime = 2f;
	private bool killCalled;

	private Rigidbody rb;
	private Renderer renderer;
	private Collider collider;

	// Use this for initialization
	void Start () {
		endTime = Time.time + killTimer;
		rb = GetComponent<Rigidbody> ();
		renderer = GetComponent<Renderer> ();
		collider = GetComponent<BoxCollider> ();
		itemCanHurt = true;
		killCalled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > endTime && !killCalled) {
			Kill ();
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player" && itemCanHurt) {
			if (rb.velocity.magnitude > 3f) {
				col.gameObject.GetComponent<PlayerMovement> ().StartKnockback (
					-(new Vector3(transform.position.x, 0, transform.position.z) -
						new Vector3(col.gameObject.transform.position.x, 0, col.gameObject.transform.position.z)), 20f);
				col.gameObject.GetComponent<PlayerMovement> ().SetCurrentHealth (col.gameObject.GetComponent<PlayerMovement> ().GetCurrentHealth () - throwDamage);
				itemCanHurt = false;
			}
		}
	}

	void Kill() {
		killCalled = true;
		itemCanHurt = false;
		rb.isKinematic = true;
		collider.isTrigger = true;
		StartCoroutine (Blink());
	}

	IEnumerator Blink() {
		float endBlinkTime = Time.time + blinkTime;
		while (Time.time < endBlinkTime) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.1f);
			renderer.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
		Destroy (gameObject);
	}
}
