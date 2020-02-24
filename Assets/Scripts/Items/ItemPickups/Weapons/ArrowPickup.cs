using UnityEngine;
using System.Collections;

public class ArrowPickup : ItemPickup
{
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.Quiver.enabled) {
			if (_gameManager.Player.Quiver.IsFull ())
				UnableToPickUp ();
			else {
				_gameManager.Player.Quiver.Add ();
				ItemPickedUp ();
				_gameManager.notify (Notification.ARROWS_CHANGED);
			}
		}
	}

}
