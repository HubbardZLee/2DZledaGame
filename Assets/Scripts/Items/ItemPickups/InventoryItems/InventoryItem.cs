using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Base class for all items to be stored in the inventory to derive from
 */
public abstract class InventoryItem : ItemPickup
{
	// Animation when the player picks this item up
	[SerializeField]
	GameObject playerPickup;

	[SerializeField]
	protected bool isReloading;
	public bool IsReloading { get { return isReloading; } }

	[SerializeField]
	protected float reloadTime;
	public float ReloadTime { get { return reloadTime; } }

	// Boolean that determines whether the player can drop this item once obtaining it
	[SerializeField]
	protected bool isDroppable;
	public bool IsDroppable { get { return isDroppable; } }

	[SerializeField]
	protected bool isSellable;
	public bool IsSellable { get { return isSellable; } }

	// Integer that stores the index of the inventory array where this item rests. Index of -1 means the item is not in the inventory
	[ReadOnly][SerializeField]
	protected int inventoryIndex = -1;
	public int InventoryIndex { get { return inventoryIndex; } }


	// Overridden Initialize function allows for better inheritance then Start
	protected override void Start () {
		base.Start ();

		playerPickup = Resources.Load ("Prefabs/PlayerPickup") as GameObject;

		tag = "InventoryItem";
		if (!isDroppable)
			pickupNoise = Resources.Load ("Audio/InventoryItemPickup") as AudioClip;  // Insert special inventoryItem noise here.
		isSnatchable = false;
		isDroppable = false;
		isSellable = false;
		isReloading = false;

		reloadTime = 1.0F;
	}

	// Will be overridden by each individual item to perform item specific logic
	public abstract void usePrimary ();
	public abstract void useSecondary ();

	protected override void AttemptToPickUp () {
		inventoryIndex = _gameManager.InventoryManager.addItem (this);

		if (inventoryIndex == -1)
			UnableToPickUp ();
		else 
			ItemPickedUp ();
	}

	// Does the same as ItemPickup except that it doesn't destroy the object so it can stay in the inventory
	protected override void ItemPickedUp () {
		if (IsDroppable) {
			GetComponent<SpriteRenderer> ().enabled = false;
			transform.parent = _gameManager.InventoryManager.transform;
			transform.position = _gameManager.InventoryManager.transform.position;
		}
		else
			StartCoroutine ("SpecialPickup");
	}

	IEnumerator SpecialPickup () {
		Time.timeScale = 0.001F;
		transform.position = _gameManager.Player.transform.position;
		transform.Translate (new Vector3 (0.0F, 0.5F, 0.0F));
		_gameManager.Player.GetComponent<SpriteRenderer> ().enabled = false;
		_gameManager.AudioManager.ItemPickupSource.clip = pickupNoise;
		_gameManager.notify(Notification.ITEM_PICKED_UP);
		GameObject go = Instantiate (playerPickup, _gameManager.Player.transform.position, Quaternion.identity) as GameObject;

		yield return new WaitForSeconds (0.0025F);

		Destroy (go);
		_gameManager.Player.GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<SpriteRenderer> ().enabled = false;
		transform.parent = _gameManager.InventoryManager.transform;
		transform.position = _gameManager.InventoryManager.transform.position;
		Time.timeScale = 1;
	}

	protected virtual IEnumerator Reload () {
		isReloading = true;
		yield return new WaitForSeconds (reloadTime);
		isReloading = false;
	}
}