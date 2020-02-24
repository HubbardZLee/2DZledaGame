using UnityEngine;
using System.Collections;

public class Door : Item {

	[SerializeField]
	Sprite lockedDoor;

	[SerializeField]
	Sprite unlockedDoor;

	[SerializeField]
	private bool locked;
	public bool Locked { get { return locked; } }

	protected override void Start () {
		base.Start ();
		tag = "Door";
		locked = true;
		GetComponent<SpriteRenderer> ().sprite = lockedDoor;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (locked) {
			if (_gameManager.Player.KeyRing.Count > 0) {
				locked = false;
				GetComponent<SpriteRenderer> ().sprite = unlockedDoor;
				GetComponent<BoxCollider2D> ().enabled = false;
				_gameManager.Player.KeyRing.Remove ();
			}
		} else {
			Debug.Log ("Put in code to move through door");
		}
	}
}
