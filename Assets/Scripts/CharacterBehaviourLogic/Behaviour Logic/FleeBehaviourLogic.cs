using UnityEngine;
using System.Collections;

public sealed class FleeBehaviourLogic : CharacterBehaviourLogic
{
	private Vector2 offset;

	public FleeBehaviourLogic (GameObject character) : base (character) { }

	public override void RunBehaviourLogic () {
		offset = _gameManager.Player.transform.position - Character.transform.position;
		Character.transform.Translate (-offset.normalized * Time.deltaTime * Character.MovementSpeed);
	}
}
