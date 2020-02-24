using UnityEngine;
using System.Collections;

public class BatsTopStatuesBottom : SetEnemiesGridArea
{
	protected override void Start () {
		base.Start ();
	}

	protected override void PopulateEnemyList () {
		for (int i = 0; i < 3; i++)
			enemyList.Add (Instantiate (Resources.Load ("Prefabs/Enemies/Bat")) as GameObject);
		for (int i = 0; i < 3; i++)
			enemyList.Add (Instantiate (Resources.Load ("Prefabs/Enemies/StatueMan")) as GameObject);

		SpawnEnemies ();
	}
}
