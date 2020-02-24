using UnityEngine;
using System.Collections;

public class Bow : InventoryItem
{
	// The arrow being shot
	GameObject arrow;

	public override void usePrimary () {
		if (!_gameManager.Player.Quiver.IsEmpty() && !isReloading) {
			arrow = Instantiate (Resources.Load("Prefabs/Weapons/Arrows/Arrow"), transform.position, Quaternion.identity) as GameObject;
			arrow.transform.parent = _gameManager.Player.transform;
			_gameManager.Player.Quiver.Remove ();
			StartCoroutine ("Reload");
		}
	}

	public override void useSecondary () {
		// Put in code for fire arrow
		if (!_gameManager.Player.Quiver.IsEmpty() && !_gameManager.Player.MatchBox.IsEmpty() && !isReloading) {
			arrow = Instantiate (Resources.Load ("Prefabs/Weapons/Arrows/FireArrow"), transform.position, Quaternion.identity) as GameObject;
			arrow.transform.parent = _gameManager.Player.transform;
			_gameManager.Player.Quiver.Remove ();
			_gameManager.Player.MatchBox.Remove ();
			StartCoroutine ("Reload");
		}
	}

	protected override void ItemPickedUp () {
		base.ItemPickedUp ();

		_gameManager.notify (Notification.ARROWS_CHANGED);
	}
}
