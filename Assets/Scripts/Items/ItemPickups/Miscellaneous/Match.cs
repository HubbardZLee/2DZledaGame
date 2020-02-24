using UnityEngine;
using System.Collections;

/*
 * A simple match pickup
 * Will increase the player's current match total by 1
 * This will inherit from ItemPickup and it's base classes
 */
public sealed class Match : ItemPickup
{
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.MatchBox.IsFull())
			UnableToPickUp ();
		else {
			_gameManager.Player.MatchBox.Add ();
			ItemPickedUp ();
			_gameManager.notify (Notification.MATCHES_CHANGED);
		}
	}
}
