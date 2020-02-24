using UnityEngine;
using System.Collections;

public class Flippers : InventoryItem
{
	public override void usePrimary () {

	}

	public override void useSecondary () {
		usePrimary ();
	}
}
