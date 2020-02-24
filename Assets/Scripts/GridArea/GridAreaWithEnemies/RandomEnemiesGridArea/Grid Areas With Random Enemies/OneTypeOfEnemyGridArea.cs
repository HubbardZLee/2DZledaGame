using UnityEngine;
using System.Collections;

public class OneTypeOfEnemyGridArea : RandomEnemiesGridArea
{
	// Type of enemy to spawn in this area
	[SerializeField]
	private GameObject enemyType;

	[SerializeField]
	int enemyCount;

	// Initialization code for the enemies
	protected override void PopulateEnemyList () {
		for (int i = 0; i < enemyCount; i++)
			enemyList.Add (Instantiate (enemyType) as GameObject);

		SpawnEnemies ();
	}
}
