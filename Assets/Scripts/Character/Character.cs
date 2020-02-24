using UnityEngine;
using System.Collections;

/*
 * Every single character in the game will have some derived version of character as a component
 * This will give access to each character's logic, rigidbody, collider, the current screen area it's in, and it's speed.
 * Every character will also have access to the GameManager
 */
public abstract class Character : MonoBehaviour
{
	protected GameManager _gameManager;

	// The character's behaviour logic
	[ReadOnly][SerializeField]
	protected CharacterBehaviourLogic behaviour;
	public CharacterBehaviourLogic Behaviour { get { return behaviour; } }

	// The character's rigidbody
	protected Rigidbody2D characterRigidbody;
	public Rigidbody2D CharacterRigidbody { get { return characterRigidbody; } }

	// The character's collider
	protected Collider2D characterCollider;
	public Collider2D CharacterCollider { get { return characterCollider; } }

	// Holds a reference to the current grid area that the player is in
	[ReadOnly][SerializeField]
	protected Collider2D currentArea;
	public Collider2D CurrentArea { get { return currentArea; } }

	// How quickly this character can move
	[SerializeField]
	protected float movementSpeed;
	public float MovementSpeed { get { return movementSpeed; } }

	void Awake () {
		_gameManager = GameManager._GameManager;
	}

	// Use this for initialization. Can be overridden by each new derived class
	protected virtual void Start () {
		behaviour = new StandStillBehaviourLogic (gameObject);	// Each character will default to standing still
		characterRigidbody = GetComponent<Rigidbody2D> ();
		characterCollider = GetComponent<Collider2D> ();

		movementSpeed = 1.0F;
	}

	// Initializes each character's current grid area.
	protected virtual void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "SetGridArea" || other.tag == "FollowGridArea")
			currentArea = other;
	}
}