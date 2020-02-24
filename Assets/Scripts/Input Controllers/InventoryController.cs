using UnityEngine;
using System.Collections;

/* 
 * This class should be placed on an object guaranteed to persist for the duration of the program.
 * Preferably an empty GUIManager object.
 */
public sealed class InventoryController : MonoBehaviour 
{
	// Create the variable to hold the reference to the GameManager object
	GameManager _gameManager;

	// Selected item. Starts at the first inventory item
	[ReadOnly][SerializeField]
	int selectedIndex = 0;
	public int SelectedIndex { get { return selectedIndex; } }

	[ReadOnly][SerializeField]
	int firstIndex = -1;
	public int FirstIndex { get { return firstIndex; } }

	// For the GUI to use to tell when an item has been selected
	[SerializeField]
	private bool isReselecting = false;
	public bool IsReselecting { get { return isReselecting; } }

	// The number of columns in the inventory GUI
	private int itemsPerRow = 4;
	public int ItemsPerRow { get { return itemsPerRow; } }

	// The number of rows in the inventory GUI
	private int itemsPerColumn = 3;
	public int ItemsPerColumn { get {return itemsPerColumn; } }

	// Use this for initialization
	void Awake () {
		// Grab the reference to the GameManager
		_gameManager = GameManager._GameManager;
	}

	/* This function will be called to handle the users input when the game's state is in Inventory mode. */
	public void handleInput() 
	{
		// Start the swap function with the selected item, swap with the second item
		if (Input.GetButtonDown ("Primary Button 1")) {
			if (firstIndex == -1)
				firstIndex = selectedIndex;
			else {
				_gameManager.InventoryManager.swapItems (firstIndex, selectedIndex);
				firstIndex = -1;
			}
		}
		// If the user selects to drop an item, then attempt to. Also, cancel any swap function if they started one.
		else if (Input.GetButtonDown ("Primary Button 2")) {
			_gameManager.InventoryManager.removeItem (selectedIndex);
			firstIndex = -1;
		}

		// Player can exit the inventory by pressing either inventory or pause
		if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Inventory")) {
			selectedIndex = 0;
			firstIndex = -1;
			Time.timeScale = 1;
			_gameManager._GameState = GameState.GAMEPLAY;
			_gameManager.notify (Notification.INVENTORY_CLOSED);
		}

		/* The movement controls will select different items for use. 
		 * IsReselecting and the 'Wait' Coroutine prevent the selection tool from moving too fast.
		 */

		// Horizontal menu movement
		if (Input.GetAxisRaw ("Horizontal") > 0 && !IsReselecting && _gameManager.InventoryManager.InventoryCount != 0) {
			if (selectedIndex >= _gameManager.InventoryManager.InventoryMaxSize - 1)
				selectedIndex = 0;
			else
				selectedIndex++;
			StartCoroutine ("Wait");
		} else if (Input.GetAxisRaw ("Horizontal") < 0 && !IsReselecting && _gameManager.InventoryManager.InventoryCount != 0) {
			if (selectedIndex <= 0)
				selectedIndex = _gameManager.InventoryManager.InventoryMaxSize - 1;
			else
				selectedIndex--;
			StartCoroutine ("Wait");
		}

		//* Vertical menu movement
		if (Input.GetAxisRaw ("Vertical") < 0 && !IsReselecting) {
			if (selectedIndex <= ItemsPerRow - 1)
				selectedIndex += ItemsPerRow * (ItemsPerColumn - 1);
			else
				selectedIndex -= ItemsPerRow;
			StartCoroutine ("Wait");
		} else if (Input.GetAxisRaw ("Vertical") > 0 && !IsReselecting) {
			if (selectedIndex >= _gameManager.InventoryManager.Inventory.Length - ItemsPerRow)
				selectedIndex -= ItemsPerRow * (ItemsPerColumn - 1);
			else
				selectedIndex += ItemsPerRow;
			StartCoroutine ("Wait");
		}
	}

	// This Coroutine forces the item selection tool to not move too quickly
	IEnumerator Wait () {
		isReselecting = true;
		for (int i = 0; i < 15; i++)
			yield return null;
		isReselecting = false;
	}
}