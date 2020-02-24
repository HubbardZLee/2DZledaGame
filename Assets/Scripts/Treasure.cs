using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {
	[SerializeField]
	AudioClip open;

	GameManager _gameManager;

	void Start () {
		_gameManager = GameManager._GameManager;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			_gameManager.AudioManager.MapAreaAudioSource.clip = open;
			_gameManager.AudioManager.MapAreaAudioSource.Play ();
			Destroy (gameObject);
		}
	}
}
