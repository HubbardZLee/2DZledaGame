using UnityEngine;
using System.Collections;

public class InventoryQuiver : ItemPickup
{
	protected override void AttemptToPickUp () {
		ItemPickedUp ();
	}

	protected override void ItemPickedUp () {
		base.ItemPickedUp ();

		_gameManager.Player.Quiver.enabled = true;
		_gameManager.Player.Quiver.InitializeContainer (9, 3);
	}
}