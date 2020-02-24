using UnityEngine;
using System.Collections;

public class Lantern : InventoryItem
{
	protected override void Start () {
		base.Start ();

		isDroppable = true;
	}

	public override void usePrimary () {
		//throw fire
	}

	public override void useSecondary () {
		// light the room
	}

	protected override void ItemPickedUp () {
		base.ItemPickedUp ();

		if (!_gameManager.Player.MatchBox.enabled) {
			_gameManager.Player.MatchBox.enabled = true;
			_gameManager.Player.MatchBox.InitializeContainer (9, 3);
			_gameManager.notify (Notification.MATCHES_CHANGED);
		}
	}
}