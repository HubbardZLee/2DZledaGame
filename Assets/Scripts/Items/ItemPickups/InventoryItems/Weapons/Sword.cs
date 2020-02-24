using UnityEngine;
using System.Collections;

public class Sword : InventoryItem
{
	[SerializeField]
	GameObject SwordSwing;

	[SerializeField]
	GameObject SwordShot;

	[SerializeField]
	AudioClip swingSword;

	protected override void Start () {
		base.Start ();

		reloadTime = 0.25F;
	}

	public override void usePrimary () {
		if (!IsReloading) {
			_gameManager.AudioManager.ItemUsedSource.clip = swingSword;
			_gameManager.AudioManager.ItemUsedSource.Play ();
			Instantiate (SwordSwing, _gameManager.Player.transform.position, Quaternion.identity);
			Debug.Log (_gameManager.Player.Health.IsFull ());
			if (_gameManager.Player.Health.IsFull ()) {
				Debug.Log ("shot");
				GameObject shot = Instantiate (SwordShot, _gameManager.Player.transform.position, Quaternion.identity) as GameObject;
				shot.transform.parent = _gameManager.Player.transform;
			}
			StartCoroutine (Reload ());
		}
	}

	public override void useSecondary () {
		usePrimary ();
	}
}
