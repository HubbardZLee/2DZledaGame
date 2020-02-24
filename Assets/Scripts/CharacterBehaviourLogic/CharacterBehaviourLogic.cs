using UnityEngine;
using System.Collections;

/*
 * 
 */
public abstract class CharacterBehaviourLogic
{
	// Grabs a reference to the GameManager for all logic to use
	protected GameManager _gameManager = GameManager._GameManager;

	// The reference to the enemy gameObject that is using this logic
	protected Character _character;
	public Character Character { get { return _character; } }

	// Grabs a reference to the Character component of the enemy using this logic
	public CharacterBehaviourLogic (GameObject thisCharacter) {
		_character = thisCharacter.GetComponent<Character> ();
	}

	// Will be run during Update of the enemy. Contains all logic code.
	public abstract void RunBehaviourLogic();
}