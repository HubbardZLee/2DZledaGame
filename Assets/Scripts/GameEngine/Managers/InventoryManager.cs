using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This class should be connected to the InventoryManager GameObject that is a child to the PlayerManager GameObject
 * The InventoryController script should be attached to the same GameObject as this
 * This class will hold the player's actual inventory in code as well as provide functions to add and remove items from it.
 * There will also be a function to swap items' positions in the player's inventory
 */
public sealed class InventoryManager : MonoBehaviour
{
	// Will hold a reference to the GameManager object
	GameManager _gameManager;

	// The inventory array to hold all of the player's items
	[ReadOnly][SerializeField]
	private InventoryItem [] inventory;
	public InventoryItem [] Inventory { get { return inventory; } }

	private int inventoryCount;
	public int InventoryCount { get { return inventoryCount; } }

	// The maximum number of items the player can carry
	[SerializeField]
	private int inventoryMaxSize;
	public int InventoryMaxSize { get { return inventoryMaxSize; } }

	// The component that controls the user input when the inventory is opened
	private InventoryController inventoryController;
	public InventoryController InventoryController { get { return inventoryController; } }

	void Awake () {
		// Grab a reference to the GameManager
		_gameManager = GameManager._GameManager;
	}

	// Initialization code
	void Start () {
		// Grab the InventoryController from the InventoryManager GameObject
		inventoryController = GetComponent<InventoryController> ();

		inventoryCount = 0;

		// Create an inventory of inventoryMaxSize size
		inventory = new InventoryItem[inventoryMaxSize];
	}


	/* 
	 * attempts to add an item to the players inventory
	 * Function returns the index where the item is stored. 
	 * Will return -1 if the item can't be added to the inventory
	 */
	public int addItem (InventoryItem item) {
		// Loop through each inventory item checking for an open spot
		for(int index = 0; index < inventoryMaxSize; index++) 
			if (inventory[index] == null) {
				inventory [index] = item;
				inventoryCount++;
				_gameManager.notify (Notification.INVENTORY_UPDATE);
				return index;
			}
		// If no open slots were found
		return -1;
	}  // End addItem


	/*
	 * attempts to remove the item at the indexed slot if it isDroppable and returns true
	 * Otherwise it does nothing and returns false
	 */
	public bool removeItem (int index) {
		if (inventory [index].IsDroppable && index != -1) {
			inventory [index].GetComponent<SpriteRenderer> ().enabled = true;
			inventory [index].transform.parent = null;
			inventory [index].transform.Translate (Vector2.left * 0.5F);
			inventory [index] = null;
			inventoryCount--;
			_gameManager.notify (Notification.INVENTORY_UPDATE);
			return true;
		} else
			return false;
	}  // End removeItem


	/*
	 * Swaps the items at the two indices
	 */
	public void swapItems (int index1, int index2) {
		InventoryItem tempItem = inventory [index1];
		inventory [index1] = inventory [index2];
		inventory [index2] = tempItem;
		_gameManager.notify (Notification.INVENTORY_UPDATE);
	}
}