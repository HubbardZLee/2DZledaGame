using UnityEngine;
using System.Collections;

public class Statue : MonoBehaviour {
	private float time;
	private bool touchingPlayer;

	GameManager _gameManager;

	[SerializeField]
	GameObject door;

	[SerializeField]
	AudioClip secret;

	void Start () {
		_gameManager = GameManager._GameManager;
	}

	void Update () {
		if (touchingPlayer) {
			time += Time.deltaTime;
			if (time > 1.0F) {
				GetComponent<Rigidbody2D> ().mass = 1;
				GetComponent<Rigidbody2D> ().drag = 1;
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-0.5F, 0.0F), ForceMode2D.Impulse);
				_gameManager.AudioManager.MapAreaAudioSource.clip = secret;
				_gameManager.AudioManager.MapAreaAudioSource.Play ();
				Destroy (door);
				StartCoroutine ("Destroy");
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
			touchingPlayer = true;
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			touchingPlayer = false;
			time = 0.0F;
		}
	}

	IEnumerator Destroy () {
		yield return new WaitForSeconds (0.1F);
		Destroy (GetComponent<Rigidbody2D> ());
		Destroy (this);
	}
}
