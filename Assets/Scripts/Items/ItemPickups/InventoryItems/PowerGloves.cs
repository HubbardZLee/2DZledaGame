using UnityEngine;
using System.Collections;

public class PowerGloves : InventoryItem
{
	public override void usePrimary () {

	}

	public override void useSecondary () {
		usePrimary ();
	}
}