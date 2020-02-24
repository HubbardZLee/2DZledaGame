using UnityEngine;
using System.Collections;

public class ActiveShield : Weapon
{
	bool isHorizontal;

	Vector3 currentPosition;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		attackDamage = 0;

		if (directionFacing == DirectionFacing.FACING_UP || directionFacing == DirectionFacing.FACING_DOWN)
			isHorizontal = true;
		else {
			isHorizontal = false;
			transform.Rotate (0.0F, 0.0F, 90.0F);
		}

		currentPosition = _gameManager.Player.transform.position;

		RepositionShield ();
	}
	
	// Update is called once per frame
	void Update () {

		currentPosition = _gameManager.Player.transform.position;

		// When the player changes direction change where the  appears
		if (directionFacing != _gameManager.Player.directionFacing) {
			RepositionShield ();
		}

		switch (directionFacing) {
		case DirectionFacing.FACING_UP:
			isHorizontal = true;
			transform.position = (currentPosition + new Vector3(0, _gameManager.Player.CharacterCollider.bounds.extents.y + 0.2F, 0));
			break;
		case DirectionFacing.FACING_RIGHT:
			isHorizontal = false;
			transform.position = (currentPosition + new Vector3(_gameManager.Player.CharacterCollider.bounds.extents.x + 0.2F, 0, 0));
			break;
		case DirectionFacing.FACING_DOWN:
			isHorizontal = true;
			transform.position = (currentPosition - new Vector3(0, _gameManager.Player.CharacterCollider.bounds.extents.y + 0.2F, 0));
			break;
		case DirectionFacing.FACING_LEFT:
			isHorizontal = false;
			transform.position = (currentPosition - new Vector3(_gameManager.Player.CharacterCollider.bounds.extents.x + 0.2F, 0, 0));
			break;
		}
	}

	void RepositionShield () {
		rotateShield ();

		directionFacing = _gameManager.Player.directionFacing;
	}

	void rotateShield () {
		if (isHorizontal && (_gameManager.Player.directionFacing == DirectionFacing.FACING_LEFT || _gameManager.Player.directionFacing == DirectionFacing.FACING_RIGHT))
			transform.Rotate (0.0F, 0.0F, 90.0F);
		else if (!isHorizontal && (_gameManager.Player.directionFacing == DirectionFacing.FACING_UP || _gameManager.Player.directionFacing == DirectionFacing.FACING_DOWN))
			transform.Rotate (0.0F, 0.0F, 90.0F);
	}
}
