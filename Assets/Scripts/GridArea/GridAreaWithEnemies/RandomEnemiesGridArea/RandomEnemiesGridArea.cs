using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class RandomEnemiesGridArea : GridAreaWithEnemies 
{
	// Used to prevent multiple enemies from spawning in the same location.
	[ReadOnly][SerializeField]
	public List<Vector3> usedLocations;

	protected override void Start () {
		base.Start ();

		usedLocations = new List<Vector3> ();
	}

	// Logic used for creating the enemies in enemyList. Override this with a different enemyList for different behaviours.
	protected override void SpawnEnemies () { 
		int randNumber;

		foreach (GameObject enemy in enemyList) {
			// Set the enemies to be direct children of the map mesh for proper positioning
			enemy.transform.parent = transform.parent;

			// Choose a random space to spawn at
			randNumber = Random.Range (0, spawnLocations.Count);
			enemy.transform.localPosition = spawnLocations [randNumber];

			// Mark that space as used and go on to the next
			usedLocations.Add (spawnLocations [randNumber]);
			spawnLocations.RemoveAt (randNumber);
		}

		// Put all the used locations back into the spawn location list
		foreach (Vector3 usedLocation in usedLocations) 
			spawnLocations.Add (usedLocation);

		// Empty the used locations list back to zero
		usedLocations.RemoveRange (0, usedLocations.Count);
	}
}
