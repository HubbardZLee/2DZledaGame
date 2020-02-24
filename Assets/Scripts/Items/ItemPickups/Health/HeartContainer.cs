using UnityEngine;
using System.Collections;

/*
 * A simple heart pickup
 * Will revive the players health by one when picked up
 * Inherits from ItemPickup and its base classes
 */
public sealed class HeartContainer : ItemPickup
{
    protected override void Start() {
		base.Start();

		isSnatchable = false;
        pickupNoise = Resources.Load("Audio/Woah") as AudioClip;
    }

	protected override void AttemptToPickUp () {
		_gameManager.Player.Health.IncreaseSize (2);
		_gameManager.Player.Health.MakeFull ();
		_gameManager.notify (Notification.HEALTH_CHANGED);
		_gameManager.notify (Notification.GAINED_CONTAINER);
		ItemPickedUp ();
    } 

}
