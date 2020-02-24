using UnityEngine;
using System.Collections;

public class SpeedBoots : InventoryItem
{
	[SerializeField]
	float usageTime;

	protected override void Start () {
		base.Start ();

		reloadTime = 300.0F;
		usageTime = 25.0F;
	}

	public override void usePrimary () {
		if (!IsReloading) 
			StartCoroutine ("UseBoots");
	}

	public override void useSecondary () {
		usePrimary ();
	}

	void PutOnBoots () {
		_gameManager.Player.PlayerController.Speed *= 1.5F;
	}

	void TakeOffBoots () {
		_gameManager.Player.PlayerController.Speed /= 1.5F;
	}


	IEnumerator UseBoots () {
		isReloading = true;
		PutOnBoots ();
		yield return new WaitForSeconds (usageTime);
		TakeOffBoots ();
		StartCoroutine (Reload ());
	}
}
