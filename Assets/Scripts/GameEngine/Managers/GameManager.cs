using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Added to allow for dynamic linked list type List<T>

/*
 * This class should hold links to every major portion of a game. Not a monobehaviour.
 * Holds read-only references to managers of Audio, GUI, Input, Inventory, Camera, and Player.
 * This is a singleton class. Meaning it cannot be created by the new keyword and only one instance
 * of this can exist. 
 * It can be accessed from any script through a read-only accessor _GameManager.
 * Just create a GameManager object and set it equal to GameManager._GameManager.
 * This object is not a MonoBehaviour and therefore exists throughout no matter what.
 * This object derives from IMessanger allowing it to send messages to all other main portions.
 */
public sealed class GameManager : IMessanger
{
	// Constructor finds the main game components and then subscribes them to receive messages.
	private GameManager () {
		player = GameObject.FindObjectOfType<Player> ();
		guiManager = GameObject.FindObjectOfType<GUIManager> ();
		audioManager = GameObject.FindObjectOfType<AudioManager> ();
		cameraManager = GameObject.FindObjectOfType<CameraManager> ();
		inventoryManager = GameObject.FindObjectOfType<InventoryManager> ();

		addObserver (guiManager);
		addObserver (audioManager);
		addObserver (cameraManager);

		CombatCharacter.InitializeDroppedItemsLists ();
	}

	// List of all the objects listening for messages. 
	private List<IObserver> observers = new List<IObserver>();

	// Make it a singleton. Only one gameManager can exist as a readonly variable accessed through its property.
	private static GameManager _gameManager = null;
	public static GameManager _GameManager {
		get {
			if (_gameManager == null)
				_gameManager = new GameManager ();
			
			return _gameManager;
		}
		private set { }
	}

	// Main Player object : Read-Only
	private Player player;																	
	public Player Player { get { return player; } }

	// Inventory manager object : Read-Only
	private InventoryManager inventoryManager;
	public InventoryManager InventoryManager { get { return inventoryManager; } }

	// Camera manager object : Read-Only
	private CameraManager cameraManager;
	public CameraManager CameraManager { get { return cameraManager; } }

	// GUI manager object : Read-Only
	private GUIManager guiManager;
	public GUIManager GUIManager { get { return guiManager; } }

	// Audio manager object : Read-Only
	private AudioManager audioManager;
	public AudioManager AudioManager { get { return audioManager; }	}


	// The current state of the game. Used mainly to determine what to do with input and GUI functions
	private GameState __gameState = GameState.GAMEPLAY;
	public GameState _GameState {
		get { return __gameState; }
		set {
			// GameState can transition from gameplay to any other state, but every other state can only transition to gameplay.
			if (value == GameState.GAMEPLAY || __gameState == GameState.GAMEPLAY)
				__gameState = value;
		}
	}


	/* Messaging system methods */
	public void addObserver (IObserver observer) {
		observers.Add (observer);
	}

	public void removeObserver (IObserver observer) {
		observers.Remove (observer);
	}

	public void notify (Notification notification) {
		Debug.Log (notification);

		foreach (IObserver observer in observers)
			observer.OnNotify (notification);
	}
}  //End GameManager


/*
 * Possible Game Play States. 
 * Can add new ones if new states are to be added. 
 */
public enum GameState
{
	GAMEPLAY,	// When the game is actually being played
	HEARTLESS,	// When the player loses all of his hearts
	PAUSED,		// When the game is simply paused. Looks like gameplay but with everything frozen
	MAP,		// When the map is open
	INVENTORY	// When the inventory is open
}