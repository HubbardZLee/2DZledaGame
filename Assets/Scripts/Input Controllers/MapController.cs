using UnityEngine;
using System.Collections;

/* 
 * This class should be placed on an object guaranteed to persist for the duration of the program.
 * Preferably an empty GUIManager object.
 */
public class MapController : MonoBehaviour 
{
	// Create the variable to hold the reference to the GameManager object
	GameManager _gameManager;

	// Use this for initialization
	void Awake () {
		//_gameManager = GameManager._GameManager;
	}

	/* This function will be called to handle the users input when the game's state is in Map mode. */
	public void handleInput()
	{
		// Place code here
	}
}