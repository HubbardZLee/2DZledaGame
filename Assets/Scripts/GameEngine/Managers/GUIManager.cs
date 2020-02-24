using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/* 
 * This component class should be placed on any object that is guaranteed to exist the duration of the game.
 * Preferably an empty object designated to hold all GUI assests. (Stephen's Health script was imported here)
 */
public class GUIManager : MonoBehaviour, IObserver
{
	// Create the variable to hold the reference to the GameManager object
	private GameManager _gameManager = null;

	public int healthPerHeart = 2;

	[SerializeField]
	private Sprite background;

	public Sprite[] heartImages;
	public GameObject heartGUI;

	private List<GameObject> hearts = new List<GameObject>();
    private Text keytext;
    private Text bombtext;
    private Text rupeetext;
	private Text matchtext;
	private Text arrowtext;

    private GameObject labelGo;
    private GameObject labelMenu;

	private Image equip1;
	private Image equip2;


	void Awake () {
		_gameManager = GameManager._GameManager;
	}


	// Used for initialization
	void Start ()
	{
		/* Grab the reference to the GameManager, InventoryController, and MapController */
        keytext = GameObject.Find("KeyCount").GetComponent<Text>();
        bombtext = GameObject.Find("BombCount").GetComponent<Text>();
        rupeetext = GameObject.Find("RupeeCount").GetComponent<Text>();
		matchtext = GameObject.Find ("MatchCount").GetComponent<Text> ();
		arrowtext = GameObject.Find ("ArrowCount").GetComponent<Text> ();
		labelGo = GameObject.Find ("InventoryLabel");
        labelMenu = GameObject.Find("MenuLabel");

        labelMenu.SetActive(false);
		labelGo.SetActive (false);

		equip1 = GameObject.Find ("Equip1").GetComponent<Image> ();
		equip2 = GameObject.Find ("Equip2").GetComponent<Image> ();


	}

	// Handles all the received messages from the GameManager
	public void OnNotify(Notification notification)
	{
		switch (notification)
		{
			case Notification.ITEM_PICKED_UP:
				UpdateEquipped ();
				break;
			case Notification.HEALTH_CHANGED:
            	case Notification.PLAYER_HAS_DIED:
				UpdateHearts(_gameManager.Player.Health.Count);
                break;
            case Notification.GAINED_CONTAINER:
                AddHearts(1);
                break;
			case Notification.MAP_OPENED:
				// Move camera to the map
				break;
			case Notification.MAP_CLOSED:
				// Camera back to the player
				break;
			case Notification.KEYS_CHANGED:
				UpdateKeys(_gameManager.Player.KeyRing.Count);
				break;
			case Notification.BOMBS_CHANGED:
				UpdateBombs(_gameManager.Player.BombBag.Count);
				break;
			case Notification.ARROWS_CHANGED:
				UpdateArrows (_gameManager.Player.Quiver.Count);
				break;
			case Notification.MATCHES_CHANGED:
				UpdateMatches (_gameManager.Player.MatchBox.Count);
				break;
			case Notification.MONEY_CHANGED:
				UpdateRupees(_gameManager.Player.Wallet.Count);
				break;
            case Notification.INVENTORY_OPENED:
                case Notification.INVENTORY_CLOSED:
                    UpdateInventory();
                break;
            case Notification.GAME_PAUSED:
                case Notification.GAME_UNPAUSED:
                    StartMenu();
                break;
			case Notification.INVENTORY_UPDATE:
				UpdateEquipped ();
				break;
        }   
	}

	public void AddHearts(int heart)
	{
		for (int i = 0; i < heart; i++)
		{
			GameObject newHeart = Instantiate(heartGUI);
			newHeart.transform.SetParent(GameObject.Find ("HeartCount").GetComponent<Transform>(), false);
			newHeart.GetComponent<Image>().sprite = heartImages [2];
			hearts.Add(newHeart);

		}
	}

	void UpdateKeys(int currentKeys){
        keytext.text = currentKeys.ToString();
	}
	void UpdateBombs(int currentBombs){
        bombtext.text = currentBombs.ToString();
	}
	void UpdateRupees(int currentRupees){
        rupeetext.text = currentRupees.ToString();
	}
	void UpdateMatches (int currentMatches) {
		matchtext.text = currentMatches.ToString ();
	}
	void UpdateArrows (int currentArrows) {
		arrowtext.text = currentArrows.ToString ();
	}

    void UpdateInventory()
    {
        if (_gameManager._GameState == GameState.INVENTORY)
        {
             labelGo.SetActive(true);
        }
        else
        {
            labelGo.SetActive(false);
        }
    }

    void StartMenu()
    {
        if (_gameManager._GameState == GameState.PAUSED)
        {
            labelMenu.SetActive(true);
        }
        else
        {
            labelMenu.SetActive(false);
        }
    }

	void UpdateEquipped () {
		if (_gameManager.InventoryManager.Inventory [0] == null)
			equip1.sprite = background;
		else
			equip1.sprite = _gameManager.InventoryManager.Inventory [0].GetComponent<SpriteRenderer> ().sprite;

		if (_gameManager.InventoryManager.Inventory [1] == null)
			equip2.sprite = background;
		else
			equip2.sprite = _gameManager.InventoryManager.Inventory [1].GetComponent<SpriteRenderer> ().sprite;
	}

	void UpdateHearts(int currentHealth) {
		bool restAreEmpty = false;

		// Iterate through each of the player's heart containers
		// For each heart check whether it should be empty, half-full, or full
		for (int i = 0; i < hearts.Count; i++) {
			// If every other heart should be empty
			if (restAreEmpty)
				hearts [i].GetComponent<Image> ().sprite = heartImages [0];

			// If the current heart should be empty, full, or half-full
			if (_gameManager.Player.Health.Count <= healthPerHeart * i) {
				hearts [i].GetComponent<Image> ().sprite = heartImages [0];
				restAreEmpty = true;
			} else if (_gameManager.Player.Health.Count >= healthPerHeart * (i + 1))
				hearts [i].GetComponent<Image> ().sprite = heartImages [2];
			else
				hearts [i].GetComponent<Image> ().sprite = heartImages [1];
		}
	}
}