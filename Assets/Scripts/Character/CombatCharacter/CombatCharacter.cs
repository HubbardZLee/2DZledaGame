using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Derived from Character class, this class will be put on each character in the game that can fight or participate in combat.
 * It will give each character a Health container, attackDamage, and booleans to determine if they are alive, frozen, or freezable.
 */
public abstract class CombatCharacter : Character
{
	// The health container of this character
	[ReadOnly][SerializeField]
	protected Health health;
	public Health Health { get { return health; } }

	// Determines if the character is still alive
	[SerializeField]
	protected bool isAlive;
	public bool IsAlive { get { return isAlive; } }

	// How much damage this character will do to others that it attacks
	[SerializeField]
	protected int attackDamage;
	public int AttackDamage { get { return attackDamage; } }

	// Can this character be frozen with freezing weapons.
	[SerializeField]
	protected bool isFreezable;
	public bool IsFreezable { get { return isFreezable; } }

	// Is this character currently frozen
	[ReadOnly][SerializeField]
	protected bool isFrozen;
	public bool IsFrozen { get { return isFrozen; } }

	// How long this character stays frozen for.
	[SerializeField]
	protected float freezeTime;
	public float FreezeTime { get { return freezeTime; } }

	// The likelyhood of this character dropping an item upon death. (Must stay between 0 and 100) 
	[SerializeField]
	protected int itemDropPercentage;
	public int ItemDropPercentage { get { return itemDropPercentage; } 
		set { Mathf.Clamp (value, 0, 100); } }

	// The type of item that this enemy will drop. (COMMON, UNCOMMON, RARE, EPIC)
	[SerializeField]
	protected ItemDropType itemDropType;
	public ItemDropType ItemDropType { get { return itemDropType; } }

	// All the lists of items that can be dropped by characters
	[ReadOnly][SerializeField]
	protected static List<string> commonDroppedItems;
	[ReadOnly][SerializeField]
	protected static List<string> uncommonDroppedItems;
	[ReadOnly][SerializeField]
	protected static List<string> rareDroppedItems;
	[ReadOnly][SerializeField]
	protected static List<string> epicDroppedItems;


	// Initialize each CombatCharacter's variables
	protected override void Start () {
		base.Start ();

		tag = "CombatCharacter";

		health = GetComponent<Health> ();

		isAlive = true;
		attackDamage = 1;
		isFreezable = true;
		isFrozen = false;
		freezeTime = 1.0F;
		itemDropPercentage = 10;
		itemDropType = ItemDropType.COMMON;
	}

	// This will be called once per frame
	protected virtual void Update () {
		// Once the character dies, determine whether to drop an item and destroy the character
		if (!isAlive) {
			if (Random.Range (0, 100) < (ItemDropPercentage + _gameManager.Player.PlayerPickupMultiplier)) {
				switch (ItemDropType) {
					case ItemDropType.COMMON:
						Instantiate (Resources.Load (commonDroppedItems [Random.Range (0, commonDroppedItems.Count)]), transform.position, Quaternion.identity);
						break;
					case ItemDropType.UNCOMMON:
						Instantiate (Resources.Load (uncommonDroppedItems [Random.Range (0, uncommonDroppedItems.Count)]), transform.position, Quaternion.identity);
						break;
					case ItemDropType.RARE:
						Instantiate (Resources.Load (rareDroppedItems [Random.Range (0, rareDroppedItems.Count)]), transform.position, Quaternion.identity);
						break;
						// Each epic item is only allowed to enter the game once
					case ItemDropType.EPIC:
						if (epicDroppedItems.Count > 0) {
							int tempNum = Random.Range (0, epicDroppedItems.Count);
							Instantiate (Resources.Load (epicDroppedItems [tempNum]), transform.position, Quaternion.identity);
							epicDroppedItems.RemoveAt (tempNum);
						}
						break;
				}
			}
			Destroy (gameObject);
		}

		UpdateCharacterLogic ();
	}  // End Update

	protected virtual void UpdateCharacterLogic () {
		behaviour.RunBehaviourLogic ();
	}

	public virtual void Attack (GameObject victim) {
		victim.GetComponent<CombatCharacter>().TakeDamage (this);
	}

	public virtual void TakeDamage (CombatCharacter attacker) {
		int damageTaken = attacker.AttackDamage;

		if (attacker.tag == "Player")
			damageTaken += _gameManager.Player.PlayerAttackMultiplier;

		if (this.tag == "Player")
			damageTaken -= _gameManager.Player.PlayerArmourMultiplier;

		Health.Remove (damageTaken);

		StartCoroutine ("HurtAnimation", attacker.gameObject);

		if (this.tag != "Player" && Health.IsEmpty())
			isAlive = false;
	}

	public virtual void TakeDamage (Weapon attackWeapon) {
		int damageTaken = attackWeapon.AttackDamage;

		if (this.tag == "Player")
			damageTaken -= _gameManager.Player.PlayerArmourMultiplier;

		Health.Remove (damageTaken);

		StartCoroutine ("HurtAnimation", attackWeapon.gameObject);

		if (this.tag != "Player" && Health.IsEmpty())
			isAlive = false;
	}

	protected virtual IEnumerator HurtAnimation (GameObject attacker) {
		CharacterRigidbody.AddForce ((transform.position - attacker.transform.position) * 10, ForceMode2D.Impulse);
		return null;
	}


	// Function that freezes the enemy if they can be frozen
	public virtual void Freeze () {
		if (IsFreezable && !IsFrozen)
			StartCoroutine ("Frozen", freezeTime);
	}

	// Function freezes the enemy by disabling their logic component for a predetermined time
	IEnumerator Frozen (float freezeTime) {
		isFrozen = true;
		this.enabled = false;
		yield return new WaitForSeconds (freezeTime);
		this.enabled = true;
		isFrozen = false;
	}


	// Called by the GameManager upon creation
	// Add prefab items to the itemType Lists
	public static void InitializeDroppedItemsLists() {
		if (commonDroppedItems == null) {
			commonDroppedItems = new List<string> ();

			commonDroppedItems.Add ("Prefabs/ItemPickups/Money/OneMoney");
			commonDroppedItems.Add ("Prefabs/ItemPickups/Money/FiveMoney");
			commonDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/HalfHeart");
			commonDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/Match");
		}

		if (uncommonDroppedItems == null) {
			uncommonDroppedItems = new List<string> ();

			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Money/TenMoney");
			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Money/TwentyMoney");
			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/Heart");
			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/Key");
			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Weapons/Bomb");
			uncommonDroppedItems.Add ("Prefabs/ItemPickups/Weapons/ArrowPickup");
		}

		if (rareDroppedItems == null) {
			rareDroppedItems = new List<string> ();

			rareDroppedItems.Add ("Prefabs/ItemPickups/Money/FiftyMoney");
			rareDroppedItems.Add ("Prefabs/ItemPickups/Money/OneHundredMoney");
			rareDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/Fairy");
			rareDroppedItems.Add ("Prefabs/ItemPickups/InventoryItem/Potion");
		}

		if (epicDroppedItems == null) {
			epicDroppedItems = new List<string> ();

			epicDroppedItems.Add ("Prefabs/ItemPickups/Miscellaneous/HeartContainer");
			epicDroppedItems.Add ("Prefabs/ItemPickups/InventoryItem/AttackPowerRing");
			epicDroppedItems.Add ("Prefabs/ItemPickups/InventoryItem/ArmourPowerRing");
			epicDroppedItems.Add ("Prefabs/ItemPickups/InventoryItem/PickupsPowerRing");
		}
	}
}



public enum ItemDropType
{
	COMMON,
	UNCOMMON,
	RARE,
	EPIC
}