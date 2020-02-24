using UnityEngine;
using System.Collections;

/*
 * A simple fairy pickup
 * Will revive the players health completely
 * Inherits from ItemPickup and its base classes
 */
public sealed class Fairy : ItemPickup
{
	protected override void Start () {
		base.Start ();

		pickupNoise = Resources.Load ("Audio/Woah") as AudioClip;
	}

	protected override void AttemptToPickUp () {
		_gameManager.Player.Health.MakeFull ();
		ItemPickedUp ();
		_gameManager.notify (Notification.HEALTH_CHANGED);
	}
}
