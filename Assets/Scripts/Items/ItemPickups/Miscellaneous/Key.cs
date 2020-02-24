using UnityEngine;
using System.Collections;

/*
 * A simple key pickup
 * Will increase the player's current keys total by 1
 * This will inherit from ItemPickup and it's base classes
 */
public sealed class Key : ItemPickup
{
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.KeyRing.IsFull())
			UnableToPickUp ();
		else {
			_gameManager.Player.KeyRing.Add ();
			ItemPickedUp ();
			_gameManager.notify (Notification.KEYS_CHANGED);
		}
	}
}
