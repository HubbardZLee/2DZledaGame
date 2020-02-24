using UnityEngine;
using System.Collections;

public class FakeStatueManEnemy : Enemy
{
	bool isMoving;

	[SerializeField]
	float pauseTime;

	protected override void Start () {
		base.Start ();

		Health.InitializeContainer (5, 5);

		movementSpeed = Random.Range (0.8F, 1.1F);
		attackDamage = 1;
		isMoving = false;

		pauseTime = 1.0F;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
			StartCoroutine ("PauseAndRethink");
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player")
			Attack (other.gameObject);
	}

	void PushTowardPlayer () {
		characterRigidbody.AddForce (_gameManager.Player.transform.position - transform.position, ForceMode2D.Impulse);
	}

	IEnumerator PauseAndRethink () {
		// Will loop throughout the duration of this enemy's life cycling between moving and being still every second.
		while (true) {
			PushTowardPlayer ();
			yield return new WaitForSeconds (pauseTime);
		}
	}
}
