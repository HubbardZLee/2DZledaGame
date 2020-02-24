using UnityEngine;
using System.Collections;

public class RemoteDetonationBomb : Weapon
{
	[SerializeField]
	Sprite explosion;

	[SerializeField]
	private bool exploded = false;
	public bool Exploded { get { return exploded; } }

	[SerializeField]
	private float explosionRadius;
	public float ExplosionRadius { get { return explosionRadius; } }

	protected override void Start () {
		base.Start ();

		attackDamage = 2;

		placeBomb ();
	}

	void placeBomb () {
		switch (_gameManager.Player.directionFacing) {
			case DirectionFacing.FACING_UP:
				transform.Translate (Vector2.up * 0.5F);
				break;
			case DirectionFacing.FACING_RIGHT:
				transform.Translate (Vector2.right * 0.5F);
				break;
			case DirectionFacing.FACING_DOWN:
				transform.Translate (Vector2.down * 0.5F);
				break;
			case DirectionFacing.FACING_LEFT:
				transform.Translate (Vector2.left * 0.5F);
				break;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && !_gameManager.Player.BombBag.IsFull()) {
			_gameManager.Player.BombBag.Add ();
			_gameManager.notify (Notification.BOMBS_CHANGED);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player" && Exploded) {
			Attack (col.gameObject);
		} else if (col.tag == "CombatCharacter" && Exploded)
			Attack (col.gameObject);
	}

	public void Detonate () { StartCoroutine ("Explode"); }

	IEnumerator Explode () {
		tag = "Bomb";
		GetComponent<SpriteRenderer> ().sprite = explosion;
		exploded = true;
		GetComponent<CircleCollider2D> ().enabled = true;
		GetComponent<CircleCollider2D> ().radius = ExplosionRadius;
		yield return new WaitForSeconds (0.15F);
		Destroy (gameObject);
	}
}
