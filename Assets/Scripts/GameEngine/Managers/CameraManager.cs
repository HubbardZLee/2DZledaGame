using UnityEngine;
using System.Collections;

/*
 * Should be placed on the main camera object
 * Pans the camera to whichever grid the player is in
 * 'Freezes' the game while moving
 * (Stephen's CameraController script was imported here)
 */
public sealed class CameraManager : MonoBehaviour, IObserver
{
	// Create the variable to hold the GameManager reference
	GameManager _gameManager;

	[SerializeField]
	float rand;

	public bool isFollowing;

	// Should the screen be moving?
	[ReadOnly][SerializeField]
	bool isMoving = false;

	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	// Initialization code
	void Start () {
		// Set the cameras initial position to be equal to the player's current grid area position
		transform.position = (Vector2)_gameManager.Player.CurrentArea.transform.position + (Vector2.up * rand);
	}

	void Update () {
		// Only attempt to move the camera if it should be moving
		if (isFollowing)
			handleInput ();
		else if (isMoving)
			for (int i = 0; i < 25; i++) // For loop allows the camera to move faster then the rest of the game
				transform.position = Vector2.MoveTowards (transform.position, (Vector2)_gameManager.Player.CurrentArea.transform.position + (Vector2.up * rand), Time.deltaTime);

	}  // End Update

	void ChangeScreen () {
		PanCamera ();
	}

	void PanCamera () {
		Time.timeScale = 0.1F;			// 'Pause' the game
		isMoving = true;				// The camera should start moving now
		StartCoroutine ("MoveCamera");	// Function to determine when the camera is done moving
	}

	void handleInput() {
		transform.position = Vector2.MoveTowards (transform.position, (Vector2) _gameManager.Player.transform.position, Time.deltaTime * 9999.0F);
	}

	// Handles messages from the GameManager
	public void OnNotify (Notification notification)
	{
		switch (notification)
		{
		// If the player enters a new area
		case Notification.ENTERED_NEW_AREA:
			isFollowing = false;
			ChangeScreen ();
			break;
		case Notification.INVENTORY_OPENED:
			Debug.Log ("Code to switch camera to inventory gui should go here");
			break;
		case Notification.MAP_OPENED:
			Debug.Log ("Code to switch camera to map mode");
			break;
		case Notification.GAME_PAUSED:
			Debug.Log ("Code to show a pause screen or menu");
			break;
		case Notification.MAP_CLOSED:
		case Notification.INVENTORY_CLOSED:
		case Notification.GAME_UNPAUSED:
			Debug.Log ("Code to switch camera to gameplay mode");
			break;
		}
	}

	// Coroutine that unpauses the game and quits moving the camera when it should
	IEnumerator MoveCamera () {
		// Loop until the camera is equal to the current grid area in 2D space
		while ((Vector2)transform.position != (Vector2)_gameManager.Player.CurrentArea.transform.position + (Vector2.up * rand))
			yield return null;
	
		// Tell the camera to quit moving and unpause the game
		isMoving = false;
		Time.timeScale = 1;
	}
}
