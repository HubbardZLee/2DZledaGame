using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This is the class for a test grid area
 * It will create four enemies randomly placed in each area when entered, and destroy them when you leave 
 */
public sealed class TestGrid : RandomEnemiesGridArea
{
	[SerializeField]
	int enemyCount;

	// Initialization code
	protected override void Start () {
		base.Start ();
	}


	protected override void PopulateEnemyList () {
		// Only creates enemies after the first screen visit.
		if (hasBeenVisited)
			while (enemyList.Count < enemyCount)
				enemyList.Add (Instantiate (Resources.Load ("Prefabs/Enemies/DumbEnemy")) as GameObject);

		// Spawn the enemies in enemyList
		SpawnEnemies ();
	}
}
