using UnityEngine;
using System.Collections;

/*
 * Will be the base class for all items in the game.
 * Allows for polymorphism.
 * Every item will have a reference to the GameManager as well as listen for messages.
 */
public abstract class Item : MonoBehaviour
{
	// Create a variable to hold the GameManager reference
	protected GameManager _gameManager;

	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	protected virtual void Start () { }
}
