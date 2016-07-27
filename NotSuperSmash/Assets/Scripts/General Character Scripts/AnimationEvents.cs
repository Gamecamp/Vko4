using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour {
	private PlayerMovement player;
	private PlayerAttackManager attackManager;
	private Bullet _bullet;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerMovement> ();
		attackManager = GetComponentInParent<PlayerAttackManager> ();
		_bullet = GetComponent<Bullet> ();
	}

	public void Activate() {
		attackManager.ActivateHitbox ();
	}

	public void Deactivate() {
		attackManager.DeactivateHitbox ();
	}

	public void End() {
		attackManager.EndCombo ();
	}

	public void Stagger() {
		player.SetIsStaggered (true);
		player.SetStaggerDuration (1.5f);
	}
}
