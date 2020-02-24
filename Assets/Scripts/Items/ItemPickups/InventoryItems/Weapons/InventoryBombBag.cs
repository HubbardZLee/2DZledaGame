using UnityEngine;
using System.Collections;

public class InventoryBombBag : InventoryItem 
{
	// Determines if there is a Remote Detonation Bomb out there to prevent multiple ones and determine when to blow up the first
	[ReadOnly][SerializeField]
	GameObject bomb;

	// Boomerang specific use code
	public override void usePrimary () {
		// Only instantiate a new boomerang once the old one is collected
		if (!_gameManager.Player.BombBag.IsEmpty()) {
			// Instantiate a boomerang prefab
			Instantiate (Resources.Load ("Prefabs/Weapons/Bombs/ThrowingBomb"), transform.position, Quaternion.identity);
			_gameManager.Player.BombBag.Remove ();
			_gameManager.notify (Notification.BOMBS_CHANGED);
		}
	}

	public override void useSecondary () {
		// Only instantiate a new boomerang once the old one is collected
		if (!_gameManager.Player.BombBag.IsEmpty() || bomb) {
			// Only instantiate a new boomerang once the old one is collected
			if (!bomb) {
				// Instantiate a stun spinning boomerang
				bomb = Instantiate (Resources.Load ("Prefabs/Weapons/Bombs/RemoteDetonationBomb"), transform.position, Quaternion.identity) as GameObject;
				_gameManager.Player.BombBag.Remove ();
				_gameManager.notify (Notification.BOMBS_CHANGED);
			} else {
				bomb.GetComponent<RemoteDetonationBomb> ().Detonate ();
			}
		}
	}

	protected override void ItemPickedUp () {
		base.ItemPickedUp ();

		_gameManager.Player.BombBag.enabled = true;
		_gameManager.Player.BombBag.InitializeContainer (9, 3);
		_gameManager.notify (Notification.BOMBS_CHANGED);
	}
}
