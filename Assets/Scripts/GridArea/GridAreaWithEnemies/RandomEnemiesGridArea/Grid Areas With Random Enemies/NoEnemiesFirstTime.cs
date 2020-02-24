using UnityEngine;
using System.Collections;

public class NoEnemiesFirstTime : RandomEnemiesGridArea
{
	[SerializeField]
	GameObject enemy;

	[SerializeField]
	int enemyCount;

	protected override void EnterGridArea () {
		if (hasBeenVisited)
			base.EnterGridArea ();
		else
			hasBeenVisited = true;
	}

	protected override void PopulateEnemyList () {
		for (int i = 0; i < enemyCount; i++)
			enemyList.Add (Instantiate (enemy) as GameObject);

		SpawnEnemies ();
	}
}
