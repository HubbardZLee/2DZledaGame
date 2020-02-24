using UnityEngine;
using System.Collections;

/* 
 * This object should be placed on the player object 
 * It will control all player movements
 */
public sealed class PlayerController : MonoBehaviour 
{
	// Reference to the game manager
	private GameManager _gameManager;

	// Variable to multiply movement by. Changeable in the Editor
	[SerializeField]
	private float speed = 10.0F;
	public float Speed { 
		get { return speed; } 
		set { if (value > 0.0F)
				speed = value; }
	}

	// Will hold input from the joysticks
	private float horizontal, vertical;

	// Grab our game manager with the static property
	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	/* Function to be called in the Update function by the Input Manager when the gamestate is set to gameplay. */ 
	public void handleInput() 
	{
		// Handles the players movement
		handleMovement ();

		/* If the player uses an equipped item 
		   'Use Item 1' is defined as 'Z' or xbox controller 'A', 'Use Item 2' as 'X' or controller 'B' */
		if (Input.GetButtonDown ("Primary Button 1") && _gameManager.InventoryManager.Inventory [0] != null)
			_gameManager.Player.AttackPrimary (0);
		else if (Input.GetButtonDown ("Primary Button 2") && _gameManager.InventoryManager.Inventory [1] != null)
			_gameManager.Player.AttackPrimary (1);
	
		/* Will not be used!!! I'm keeping it here in case we switch to 4 equippable items. Now or in 427. 
		 * 'Use Item 3' is defined as 'C' or xbox controller 'X', 'Use Item 4' as 'V' or controller 'Y' */
		else if (Input.GetButtonDown ("Secondary Button 1") && _gameManager.InventoryManager.Inventory [0] != null)
			_gameManager.Player.AttackSecondary (0);
		else if (Input.GetButtonDown ("Secondary Button 2") && _gameManager.InventoryManager.Inventory [1] != null)
			_gameManager.Player.AttackSecondary (1);

		/* If the player decides to open a different menu, Game state is changed, thus passing input control
		 * to that specific game state's input controller. The switch is done in the input manager. */
		// When the game is paused time will freeze, game state changed to PAUSED and every system notified
		else if (Input.GetButtonDown ("Pause")) 
		{
			Debug.Log ("Game is paused");
			Time.timeScale = 0;
			_gameManager._GameState = GameState.PAUSED;
			_gameManager.notify (Notification.GAME_PAUSED);
			return;
		} 
		/*else if (Input.GetButtonDown ("Minimap"))
		{
			Debug.Log ("Minimap is open");
			Time.timeScale = 0;
			_gameManager._GameState = GameState.MAP;
			_gameManager.notify (Notification.MAP_OPENED);
			return;
		}*/
		else if (Input.GetButtonDown ("Inventory"))
		{
			Debug.Log ("Inventory menu is open");
			Time.timeScale = 0;
			_gameManager._GameState = GameState.INVENTORY;
			_gameManager.notify (Notification.INVENTORY_OPENED);
			return;
		}
		else if (Input.GetButtonDown ("Secondary Weapon Switch Forward"))
		{
			for (int i = _gameManager.InventoryManager.InventoryCount - 1; i > 1; i--)
				_gameManager.InventoryManager.swapItems (i, i - 1);
		}
		else if (Input.GetButtonDown ("Secondary Weapon Switch Backward"))
		{
			for (int i = 1; i < _gameManager.InventoryManager.InventoryCount - 1; i++)
				_gameManager.InventoryManager.swapItems (i, i + 1);
		}
	}

	/* Handles all of the players movement */
	private void handleMovement ()
	{
		// This will be used if the player is using a joystick for control
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");
	
		// Move the player
		/*transform.Translate (Time.deltaTime * speed * horizontal, -Time.deltaTime * speed * vertical, 0.0F);*/
		_gameManager.Player.CharacterRigidbody.AddForce (new Vector2 (Time.deltaTime * speed * horizontal, -Time.deltaTime * speed * vertical), ForceMode2D.Impulse);
	
		// If the player is not moving, animate them
		if (horizontal == 0.0F && vertical == 0.0F)
			_gameManager.Player.Animator.SetBool ("Moving", false);
		else {
			_gameManager.Player.Animator.SetBool("Moving", true);
			AnimateWalk(horizontal, vertical);
		}
	}

	void AnimateWalk(float horizontal, float vertical)
	{
		if (_gameManager.Player.Animator.GetBool("Moving"))
		{
			if (vertical < 0) {
				_gameManager.Player.Animator.SetInteger ("Direction", 1);
				_gameManager.Player.directionFacing = DirectionFacing.FACING_UP;
			} else {
				_gameManager.Player.Animator.SetInteger ("Direction", 3);
				_gameManager.Player.directionFacing = DirectionFacing.FACING_DOWN;
			}

			// Horizontal animation will always dominate over the vertical animation
			if (horizontal > 0) {
				_gameManager.Player.Animator.SetInteger ("Direction", 2);
				_gameManager.Player.directionFacing = DirectionFacing.FACING_RIGHT;
			} else if (horizontal < 0) {
				_gameManager.Player.Animator.SetInteger ("Direction", 4);
				_gameManager.Player.directionFacing = DirectionFacing.FACING_LEFT;
			}
		}
	}
}
