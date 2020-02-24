using UnityEngine;
using System.Collections;

/*
 * This class inherits from ItemPickup and its base classes.
 * This will be the base class for all different denominations of money.
 */
public abstract class Money : ItemPickup
{
	// Amount of money that this object is worth
	protected int moneyValue;

	protected override void Start () {
		base.Start ();

		pickupNoise = Resources.Load ("Audio/ChaChing") as AudioClip;
	}

	protected override void AttemptToPickUp () {
		if (_gameManager.Player.Wallet.IsFull())
			UnableToPickUp ();
		else {
			_gameManager.Player.Wallet.Add (moneyValue);
			ItemPickedUp ();
			_gameManager.notify (Notification.MONEY_CHANGED);
		}
	}
}
