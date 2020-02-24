using UnityEngine;
using System.Collections;

public class TwoTypesOfEnemiesGridArea : RandomEnemiesGridArea
{
	// The first enemy type and how many
	[SerializeField]
	private GameObject enemyTypeOne;
	[SerializeField]
	private int numOfEnemyTypeOne;

	// The second enemy type and how many
	[SerializeField]
	private GameObject enemyTypeTwo;
	[SerializeField]
	private int numOfEnemyTypeTwo;

	protected override void PopulateEnemyList () {
		for (int i = 0; i < numOfEnemyTypeOne; i++)
			enemyList.Add (Instantiate (enemyTypeOne) as GameObject);
		for (int i = 0; i < numOfEnemyTypeTwo; i++)
			enemyList.Add (Instantiate (enemyTypeTwo) as GameObject);

		SpawnEnemies ();
	}
}
