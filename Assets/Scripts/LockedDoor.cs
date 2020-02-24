using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour {
	GameManager _gameManager;

	[SerializeField]
	GameObject door;

	void Start () {
		_gameManager = GameManager._GameManager;
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && !_gameManager.Player.KeyRing.IsEmpty()) {
			_gameManager.Player.KeyRing.Remove (1);
			_gameManager.notify (Notification.KEYS_CHANGED);
			door.transform.Translate (Vector3.forward * 10);
			Destroy (gameObject);
		}
	}
}
