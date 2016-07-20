using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour {
	private PlayerAttackManager attackManager;

	// Use this for initialization
	void Start () {
		attackManager = GetComponentInParent<PlayerAttackManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeAttackPhase(string to) {
		attackManager.ChangeAttackPhase (to);
	}
}
