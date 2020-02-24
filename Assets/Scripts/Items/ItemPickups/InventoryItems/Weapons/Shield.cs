using UnityEngine;
using System.Collections;

public class Shield : InventoryItem
{
	GameObject shield;

	public override void usePrimary () {
		if (shield) 
			Destroy (shield);
		else 
			shield = Instantiate (Resources.Load ("Prefabs/Weapons/Shield")) as GameObject;
	}

	public override void useSecondary () {
		usePrimary ();
	}
}
