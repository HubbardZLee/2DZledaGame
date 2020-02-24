using UnityEngine;
using System.Collections;

/*
 * This class will inherit from the base Item class and therefore MonoBehaviour and IObserver as well.
 * Item Pickup represent Items in the game world that the player picks up but doesn't keep. 
 * They are instantly used. Ex: Hearts, Bombs, Matches, Money, and Keys.
 * They will still attemptToPickup before actually using so that they aren't just wasted
 * Still a work in progress. IGNORE FOR NOW!
 */
public abstract class ItemPickup : Item
{
	// Determines whether items like the boomerang and hookshot can grab this item for the player
	protected bool isSnatchable;
	public bool IsSnatchable { get { return isSnatchable; } }

	// Generic noise to play when item can't be picked up
	[SerializeField]
	protected AudioClip unableToPickup;

	// Overridden noise to play when item is picked up
	[SerializeField]
	protected AudioClip pickupNoise;

	protected override void Start () {
		base.Start ();

		tag = "ItemPickup";
		unableToPickup = Resources.Load ("Audio/ItemPickups/UnableToPickupItem") as AudioClip;
		isSnatchable = true;
	}


	protected virtual void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
			AttemptToPickUp ();
	}

	protected abstract void AttemptToPickUp ();

	// Called if the Item Pickup can't be picked up
	protected virtual void UnableToPickUp () {
		_gameManager.AudioManager.ItemPickupSource.clip = unableToPickup;
		_gameManager.notify(Notification.UNABLE_TO_PICK_UP_ITEM);
	}

	// Called when the item is picked up
	protected virtual void ItemPickedUp () {
		_gameManager.AudioManager.ItemPickupSource.clip = pickupNoise;
		_gameManager.notify(Notification.ITEM_PICKED_UP);
		Destroy(this.gameObject);
	}
}