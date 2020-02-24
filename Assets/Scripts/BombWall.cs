using UnityEngine;
using System.Collections;

public class BombWall : MonoBehaviour {

	GameManager _gameManager;

	[SerializeField]
	AudioClip secret;

	void Start () {
		_gameManager = GameManager._GameManager;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Bomb") {
			_gameManager.AudioManager.MapAreaAudioSource.clip = secret;
			_gameManager.AudioManager.MapAreaAudioSource.Play ();
			Destroy (gameObject);
		}
	}
}
