using UnityEngine;
using System.Collections;

public sealed class SeekBehaviourLogic : CharacterBehaviourLogic
{
	// Will hold the vector offset from the enemy to the player
	private Vector2 offset;

	// Necessary
	public SeekBehaviourLogic (GameObject character) : base (character) { }

	// Seek behaviour logic
	public override void RunBehaviourLogic () {
		offset = _gameManager.Player.transform.position - Character.transform.position;
		Character.transform.Translate (offset.normalized * Time.deltaTime * Character.MovementSpeed);
	}
}
