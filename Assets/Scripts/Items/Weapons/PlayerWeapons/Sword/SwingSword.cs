using UnityEngine;
using System.Collections;

public class SwingSword : Weapon 
{
	SpriteRenderer sr;

	[SerializeField]
	Sprite [] upDirection;
	[SerializeField]
	Sprite [] rightDirection;
	[SerializeField]
	Sprite [] downDirection;
	[SerializeField]
	Sprite [] leftDirection;

	protected override void Start () {
		base.Start ();

		attackDamage = 1;

		transform.parent = _gameManager.Player.transform;

		sr = GetComponent<SpriteRenderer> ();

		StartCoroutine ("SwingingSword");
	}

	void UpdateAnimation (int swingStage) {
		switch (_gameManager.Player.directionFacing) {
		case DirectionFacing.FACING_UP:
			sr.sprite = upDirection [swingStage];
			break;
		case DirectionFacing.FACING_RIGHT:
			sr.sprite = rightDirection [swingStage];
			break;
		case DirectionFacing.FACING_DOWN:
			sr.sprite = downDirection [swingStage];
			break;
		case DirectionFacing.FACING_LEFT:
			sr.sprite = leftDirection [swingStage];
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "CombatCharacter") {
			Attack (other.gameObject);
			attackDamage = 0;
		}
	}

	IEnumerator SwingingSword () {
		_gameManager.Player.GetComponent<SpriteRenderer> ().enabled = false;

		for (int i = 0; i < 6; i++) {
			yield return new WaitForSeconds (0.025F);
			UpdateAnimation (i);
		}

		_gameManager.Player.GetComponent<SpriteRenderer> ().enabled = true;
		Destroy (gameObject);
	}
}
