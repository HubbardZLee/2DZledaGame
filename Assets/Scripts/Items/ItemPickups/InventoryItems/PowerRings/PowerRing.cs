using UnityEngine;
using System.Collections;

public abstract class PowerRing : InventoryItem 
{
	[SerializeField]
	protected float ringUseTime = 10.0F;

	protected override void Start () {
		base.Start ();

		reloadTime = 300.0F;
	}

	public override void usePrimary () {
		if (!IsReloading)
			StartCoroutine ("UseRing");
	}

	public override void useSecondary () {
		usePrimary ();
	}

	protected abstract void ringPutOn();
	protected abstract void ringTakenOff();

	protected IEnumerator UseRing () {
		isReloading = true;
		ringPutOn ();
		yield return new WaitForSeconds (ringUseTime);
		ringTakenOff ();
		StartCoroutine (Reload ());
	}
}
