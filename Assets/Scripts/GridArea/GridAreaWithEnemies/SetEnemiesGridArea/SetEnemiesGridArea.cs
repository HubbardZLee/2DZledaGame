using UnityEngine;
using System.Collections;

/*
 * 
 */
public abstract class SetEnemiesGridArea : GridAreaWithEnemies
{
	// Logic used for creating the enemies in enemyList. Override this with a different enemyList for different behaviours.
	protected override void SpawnEnemies () { 
		int i = 0;

		foreach (GameObject enemy in enemyList) {
			// Set the enemies to be direct children of the map mesh for proper positioning
			enemy.transform.parent = transform.parent;

			// Spawn them in sequential order. Left to Right, Top to Bottom
			enemy.transform.localPosition = spawnLocations [i];

			//if (i == spawnLocations.Count)
			//	break;

			i++;
		}
	}
}
