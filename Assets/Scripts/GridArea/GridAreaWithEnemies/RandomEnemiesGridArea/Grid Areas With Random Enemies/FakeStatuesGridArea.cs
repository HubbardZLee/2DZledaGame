using UnityEngine;
using System.Collections;

public class FakeStatuesGridArea : RandomEnemiesGridArea
{
	[SerializeField]
	GameObject enemy;
	[SerializeField]
	int numEnemies;

	[SerializeField]
	GameObject statue;
	[SerializeField]
	int numStatues;

	protected override void PopulateEnemyList () {
		for (int i = 0; i < numEnemies; i++)
			enemyList.Add (Instantiate (enemy) as GameObject);
		for (int i = 0; i < numStatues; i++)
			enemyList.Add (Instantiate (statue) as GameObject);

		SpawnEnemies ();
	}
}
