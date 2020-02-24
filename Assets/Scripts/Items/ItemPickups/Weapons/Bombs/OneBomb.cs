using UnityEngine;
using System.Collections;

/*
 * A simple bomb pickup
 * Will add a bomb to the player's bomb bag
 * Inherits from ItemPickup and its base classes
 */
public class OneBomb : ItemPickup
{
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.BombBag.enabled) {
			if (_gameManager.Player.BombBag.IsFull ())
				UnableToPickUp ();
			else {
				_gameManager.Player.BombBag.Add ();
				ItemPickedUp ();
				_gameManager.notify (Notification.BOMBS_CHANGED);
			}
		}
	}
}
