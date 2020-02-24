using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 
 */
public abstract class GridAreaWithEnemies : GridArea
{
	// List of enemies to spawn in this grid area
	[ReadOnly][SerializeField]
	protected List<GameObject> enemyList;

	// List is used to help facilitate the removal of enemies from the main enemylist
	protected List<GameObject> tempEnemyList;

	// List of Vector3's that enemies are allowed to randomly spawn at for this grid area
	[ReadOnly][SerializeField]
	protected List<Vector3> spawnLocations;

	protected override void Start () {
		base.Start ();

		enemyList = new List<GameObject> ();
		tempEnemyList = new List<GameObject> ();
		spawnLocations = new List<Vector3> ();

		// Temporary array with all spawn location vertices. 
		Vector3 [] SpawnLocations = GetComponentInParent<MeshFilter> ().mesh.vertices;

		// We only want one Vector3 per location. This will add the lower left Vertex of each box location
		for (int i = 0; i < SpawnLocations.Length; i++)
			if (i % 4 == 0)
				spawnLocations.Add (SpawnLocations [i]);
	}
		
	// Called when the player enters this area
	protected override void EnterGridArea () {
		base.EnterGridArea ();

		PopulateEnemyList ();
	}

	// Called when the player exits the grid area
	protected override void ExitGridArea () {
		DestroyEnemies ();
	}

	// Used for enemy Initialization code. Populate the enemyList
	protected abstract void PopulateEnemyList ();

	// Used to actually spawn the enemies. Random or Set. etc...
	protected abstract void SpawnEnemies ();

	// Destroys all of this grids enemies
	protected virtual void DestroyEnemies () {
		// Store each object in the temp list so that I can iterate through that list while removing from the main list
		foreach (GameObject enemy in enemyList)
			tempEnemyList.Add (enemy);
		foreach (GameObject enemy in tempEnemyList) {
			enemyList.Remove (enemy);
			Destroy (enemy);
		}
	}
}
