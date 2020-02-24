using UnityEngine;
using System.Collections;


public sealed class Boomerang : InventoryItem 
{
	// Prevents multiple boomerangs from being instantiated at once
	[ReadOnly][SerializeField]
	GameObject boomerang;

	// Boomerang specific use code
	public override void usePrimary () {
		// Only instantiate a new boomerang once the old one is collected
		if (!boomerang) {
			// Instantiate a boomerang prefab
			boomerang = Instantiate (Resources.Load ("Prefabs/Weapons/Boomerangs/KillStraightBoomerang"), transform.position, Quaternion.identity) as GameObject;
			boomerang.transform.parent = _gameManager.Player.transform;
		}
	}

	public override void useSecondary () {
		// Only instantiate a new boomerang once the old one is collected
		if (!boomerang) {
			// Instantiate a stun spinning boomerang
			boomerang = Instantiate (Resources.Load ("Prefabs/Weapons/Boomerangs/KillSpinningBoomerang"), transform.position, Quaternion.identity) as GameObject;
			boomerang.transform.parent = _gameManager.Player.transform;
		}
	}

}
