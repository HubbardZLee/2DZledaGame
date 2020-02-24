using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySpot : MonoBehaviour 
{
	GameManager _gameManager;

	Image image;

	Sprite background;

	Button myButton;

	[SerializeField]
	int inventorySpot;

	Color color;

	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	void Start () {
		myButton = GetComponent<Button> ();
		myButton.enabled = false;

		image = GetComponent<Image> ();
		background = image.sprite;
	}

	void Update () {
		if (_gameManager.InventoryManager.Inventory [inventorySpot] != null)
			image.sprite = _gameManager.InventoryManager.Inventory [inventorySpot].GetComponent<SpriteRenderer> ().sprite;
		else
			image.sprite = background;

		if (_gameManager.Player.InventoryManager.InventoryController.FirstIndex == inventorySpot)
			image.color = Color.red;
		else if (_gameManager.Player.InventoryManager.InventoryController.SelectedIndex == inventorySpot)
			image.color = Color.green;
		else
			image.color = Color.white;
	}
}
