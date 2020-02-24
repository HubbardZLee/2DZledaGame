using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Added to allow for dynamic linked list type List<T>


public enum DirectionFacing 
{
	FACING_UP,
	FACING_RIGHT,
	FACING_DOWN,
	FACING_LEFT
}


/*
 * This script should be added to the player object. If the Player object is ever to be deleted
 * then look for another way or remember to re-add this component through the GameManager during game.
 * This object will both send and receive messages.
 * Will hold all necessary player variables and set limits on how they can be set.
 */
public sealed class Player : CombatCharacter
{
	[SerializeField]
	AudioClip hurt;

	// Determines whether the player has been hurt. If so, the player can't be hurt again until set back to false.
	[SerializeField]
	bool invincible;
	public bool Invincible { get { return invincible; } }

	// Determines how long the player has to recover from being hit
	[SerializeField]
	float recoveryTime = 1.5F;
	public float RecoveryTime { get { return recoveryTime; } }

	// Tells which direction the player is facing
	[ReadOnly][SerializeField]
	public DirectionFacing directionFacing;
	DirectionFacing fallDirection;

	// When worn it will increase the player's attacking damage
	[SerializeField]
	int playerAttackMultiplier = 0;
	public int PlayerAttackMultiplier {
		get { return playerAttackMultiplier; } 
		set { if (value == (playerAttackMultiplier + 1) || (value == (playerAttackMultiplier - 1) && value >= 0))
			playerAttackMultiplier = value; }
	}

	// When worn it will increase the player's resistance to damage
	[SerializeField]
	int playerArmourMultiplier = 0;
	public int PlayerArmourMultiplier {
		get { return playerArmourMultiplier; } 
		set { if (value == (playerArmourMultiplier + 1) || (value == (playerArmourMultiplier - 1) && value >= 0))
			playerArmourMultiplier = value; }
	}

	// When worn it will increase the player's chances of having items drop from the enemy
	[SerializeField]
	int playerPickupMultiplier = 0;
	public int PlayerPickupMultiplier {
		get { return playerPickupMultiplier; } 
		set { if (value == (playerPickupMultiplier + 10) || (value == (playerPickupMultiplier - 10) && value >= 0))
			playerPickupMultiplier = value; }
	}

	// All the container items that the player can carry.
	ContainerItem bombBag, wallet, quiver, keyRing, matchBox;
	public ContainerItem BombBag { get { return bombBag; } }
	public ContainerItem Wallet { get { return wallet; } }
	public ContainerItem Quiver { get { return quiver; } }
	public ContainerItem KeyRing { get { return keyRing; } }
	public ContainerItem MatchBox { get { return matchBox; } }


	// The PlayerController deals with input when the game is playing : Read-Only
	PlayerController playerController;
	public PlayerController PlayerController { get { return playerController; } }

	// The Player's inventory is controlled through the inventory manager
	InventoryManager inventoryManager;
	public InventoryManager InventoryManager { get { return inventoryManager; } }

	// Holds a reference to the player's animation controller
	Animator animator;
	public Animator Animator { get { return animator; } }

	SpriteRenderer spriteRender;

	// Initialization code
	protected override void Start() {
		base.Start ();

		// The player's inventory
		inventoryManager = GetComponentInChildren<InventoryManager> ();
		
		// The player manager needs to hold a reference to the PlayerController
		playerController = GetComponent<PlayerController> ();	
		
		// Grabs the reference to the player animation
		animator = GetComponent<Animator> ();

		spriteRender = GetComponent<SpriteRenderer> ();

		currentArea = GameObject.Find ("Entrance Room").GetComponent<Collider2D> ();

		tag = "Player";

		recoveryTime = 1.5F;

		wallet = GetComponent<Wallet> ();
		keyRing = GetComponent<KeyRing> ();
		matchBox = GetComponent<MatchBox> ();
		bombBag = GetComponent<BombBag> ();
		quiver = GetComponent<Quiver> ();
		bombBag.enabled = false;
		quiver.enabled = false;
		matchBox.enabled = false;

		Health.InitializeContainer (6, 6);
		Wallet.InitializeContainer (999, 0);
		KeyRing.InitializeContainer (5, 5);

		_gameManager.GUIManager.AddHearts (Health.Capacity / 2);
	}

	// Notifies the camera when the player moves to different areas.
	new void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "SetGridArea" && col != currentArea) {
			currentArea = col;
			_gameManager.notify (Notification.ENTERED_NEW_AREA);
		} else if (col.tag == "FollowGridArea" && col != currentArea) {
			currentArea = col;
			_gameManager.CameraManager.isFollowing = true;
		}
	}

	public override void TakeDamage (CombatCharacter attacker) {
		if (!Invincible) {
			_gameManager.AudioManager.PlayerAudioSource.clip = hurt;
			_gameManager.AudioManager.PlayerAudioSource.Play ();
			base.TakeDamage (attacker);
			StartCoroutine (Hurt());
		}
	}

	public override void TakeDamage (Weapon attackWeapon) {
		if (!Invincible) {
			_gameManager.AudioManager.PlayerAudioSource.clip = hurt;
			_gameManager.AudioManager.PlayerAudioSource.Play ();
			base.TakeDamage (attackWeapon);
			StartCoroutine (Hurt());
		}
	}

	// Uses the Primary attack of whichever weapon sits at Inventory[index]
	public void AttackPrimary (int index) {
		InventoryManager.Inventory [index].usePrimary ();
	}

	// Uses the Secondary attack of whichever weapon sits at Inventory[index]
	public void AttackSecondary (int index) {
		InventoryManager.Inventory [index].useSecondary ();
	}

	// When the player gets hurt, he will turn invincible for a fraction of time before he can be hit again. (Prevents instant death)
	IEnumerator Hurt () {
		_gameManager.notify (Notification.HEALTH_CHANGED);

		invincible = true;
		StartCoroutine ("FlashHurt");
		yield return new WaitForSeconds (RecoveryTime);
		invincible = false;
	}

	IEnumerator FlashHurt () {
		while (invincible) {
			spriteRender.enabled = false;
			yield return new WaitForSeconds (0.015F);
			spriteRender.enabled = true;
			yield return new WaitForSeconds (0.025F);
		}
	}
}