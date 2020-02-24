using UnityEngine;
using System.Collections;

/*
 * A simple heart pickup
 * Will revive the players health by one when picked up
 * Inherits from ItemPickup and its base classes
 */
public sealed class HalfHeart : ItemPickup
{
	protected override void Start () {
		base.Start ();

		pickupNoise = Resources.Load ("Audio/Woah") as AudioClip;
	}
		
	protected override void AttemptToPickUp () {
		if (_gameManager.Player.Health.IsFull())
			UnableToPickUp ();
		else {
			_gameManager.Player.Health.Add ();
			ItemPickedUp ();
			_gameManager.notify (Notification.HEALTH_CHANGED);
		}
	}
}
