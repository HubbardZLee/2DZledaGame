using UnityEngine;
using System.Collections;

/* 
 * This component class should be placed on any object that is guaranteed to exist the duration of the game.
 * Preferably an empty object designated to hold all managing scripts.
 * It sends user input to a different input controller depending on the _gameState
 */
public class InputManager : MonoBehaviour
{
	GameManager _gameManager;

	// Initialization code
	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	// Called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) || (Input.GetButton ("Pause") && Input.GetButton ("Inventory")))
			Application.Quit ();

		// Check to see what state the game is in to determine what the user input will do.
		switch (_gameManager._GameState) {
			// If the game is currently under play, then the PlayerController should handle user input.
			case GameState.GAMEPLAY:
				_gameManager.Player.PlayerController.handleInput();
				break;

			// If the game is currently in Inventory view, then the InventoryController should handle user input.
			case GameState.INVENTORY:
				_gameManager.InventoryManager.InventoryController.handleInput ();
				break;

			// If the game is currently in Map view, then the game should be paused and controls should deal with the map.
			case GameState.MAP:
				_gameManager.Player.PlayerController.handleInput ();	
				//_gameManager.CameraManager.MapController.handleInput();
				break;

			// If the game is currently paused then only check for when the player unpauses.
			case GameState.PAUSED:
				// If the player presses pause while the game is paused, then unfreeze game and notify everyone
				if (Input.GetButtonDown("Pause")) {
					_gameManager._GameState = GameState.GAMEPLAY;
					_gameManager.notify (Notification.GAME_UNPAUSED);
					Time.timeScale = 1;
				}
				break;
/*
			// Control should never reach here. Print comment for debugging purposes if so
			default:
				Debug.Log ("You reached the default case of the InputManager's Update function.");
				Debug.Log ("GameManager._GameState is invalid.");
				break;*/
		}  // End Switch
	}  // End Update
}
