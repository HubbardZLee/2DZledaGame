using UnityEngine;
using System.Collections;

public sealed class DumbEnemy : Enemy
{
	protected override void Start () {
		base.Start ();
	
		Health.InitializeContainer (3, 3);

		itemDropType = ItemDropType.COMMON;
		itemDropPercentage = 100;
		isFreezable = true;
		isFrozen = false;

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


