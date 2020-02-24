using UnityEngine;
using System.Collections;

public class LungeBehaviour : CharacterBehaviourLogic
{
	private Vector2 offset;

	public LungeBehaviour (GameObject character) : base (character) { }

	// lunge behaviour logic
	public override void RunBehaviourLogic () {
		offset = _gameManager.Player.transform.position - Character.transform.position;
		Character.transform.Translate (offset.normalized * Time.deltaTime * Character.MovementSpeed);
	}
}
