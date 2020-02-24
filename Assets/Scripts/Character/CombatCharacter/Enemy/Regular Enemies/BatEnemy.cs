using UnityEngine;
using System.Collections;

public sealed class BatEnemy : Enemy
{
	protected override void Start () {
		base.Start ();

		Health.InitializeContainer (1, 1);

		movementSpeed = Random.Range (0.4F, 0.75F);
		itemDropPercentage = 50;
		itemDropType = ItemDropType.COMMON;
		isFreezable = true;


		behaviour = new SeekBehaviourLogic (gameObject);
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Attack (other.gameObject);
			StartCoroutine ("HitAndRun");
		}
	}

	IEnumerator HitAndRun () {
		behaviour = new FleeBehaviourLogic (gameObject);
		yield return new WaitForSeconds (1.0F);
		behaviour = new SeekBehaviourLogic (gameObject);
	}
}
