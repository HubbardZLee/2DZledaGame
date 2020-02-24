using UnityEngine;
using System.Collections;

public class FullHealthPotion : InventoryItem
{
	protected override void Start () {
		base.Start ();

		isDroppable = true;
		isSellable = true;
	}

	public override void usePrimary () {
		_gameManager.Player.Health.MakeFull ();
		_gameManager.Player.InventoryManager.removeItem (inventoryIndex);
		_gameManager.notify (Notification.INVENTORY_UPDATE);
		_gameManager.notify (Notification.HEALTH_CHANGED);
		Destroy (gameObject);
	}

	public override void useSecondary () {
		usePrimary ();
	}
}
