using UnityEngine;
using System.Collections;

public sealed class ThreeBomb : ItemPickup
{
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.BombBag.enabled) {
			if (_gameManager.Player.BombBag.IsFull())
				UnableToPickUp ();
			else {
				_gameManager.Player.BombBag.Add (3);
				ItemPickedUp ();
				_gameManager.notify (Notification.BOMBS_CHANGED);
			}
		}
	}
}