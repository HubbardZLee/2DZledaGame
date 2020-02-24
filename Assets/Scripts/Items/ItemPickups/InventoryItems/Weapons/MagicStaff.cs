using UnityEngine;
using System.Collections;

public class MagicStaff : InventoryItem
{
	// Fire ball
	public override void usePrimary () {
		if (!IsReloading && !_gameManager.Player.MatchBox.IsEmpty()) {
			GameObject fireBall = Instantiate (Resources.Load ("Prefabs/Weapons/MagicStaff/FireBall"), _gameManager.Player.transform.position, Quaternion.identity) as GameObject;
			fireBall.transform.parent = _gameManager.Player.transform;
			_gameManager.Player.MatchBox.Remove ();
			_gameManager.notify (Notification.MATCHES_CHANGED);
			StartCoroutine (Reload ());
		}
	}

	// Freeze ball
	public override void useSecondary () {
		if (!IsReloading) {
			Instantiate (Resources.Load ("Prefabs/Weapons/MagicStaff/IceBall"), _gameManager.Player.transform.position, Quaternion.identity);
			StartCoroutine (Reload ());
		}
	}
}
