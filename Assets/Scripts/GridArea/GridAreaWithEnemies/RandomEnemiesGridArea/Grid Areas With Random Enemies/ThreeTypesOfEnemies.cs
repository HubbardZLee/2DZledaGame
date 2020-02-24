using UnityEngine;
using System.Collections;

public class ThreeTypesOfEnemies : RandomEnemiesGridArea
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

	// The third enemy type and how many
	[SerializeField]
	private GameObject enemyTypeThree;
	[SerializeField]
	private int numOfEnemyTypeThree;

	protected override void PopulateEnemyList () {
		for (int i = 0; i < numOfEnemyTypeOne; i++)
			enemyList.Add (Instantiate (enemyTypeOne) as GameObject);
		for (int i = 0; i < numOfEnemyTypeTwo; i++)
			enemyList.Add (Instantiate (enemyTypeTwo) as GameObject);
		for (int i = 0; i < numOfEnemyTypeThree; i++)
			enemyList.Add (Instantiate (enemyTypeThree) as GameObject);

		SpawnEnemies ();
	}
}
