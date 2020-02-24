using UnityEngine;
using System.Collections;

public class IceBall : Weapon
{
	Rigidbody2D rb;

	[SerializeField]
	private float speed;

	protected override void Start () {
		base.Start ();

		rb = GetComponent<Rigidbody2D> ();

		SpawnBall ();
	}

	void SpawnBall() {
		switch (directionFacing) {
			case DirectionFacing.FACING_UP:
				transform.Rotate (0.0F, 0.0F, 90.0F);
				rb.AddForce (new Vector2(0.0F, speed), ForceMode2D.Force);
				break;
			case DirectionFacing.FACING_RIGHT:
				rb.AddForce (new Vector2(speed, 0.0F), ForceMode2D.Force);
				break;
			case DirectionFacing.FACING_DOWN:
				transform.Rotate (0.0F, 0.0F, 270.0F);
				rb.AddForce (new Vector2(0.0F, -speed), ForceMode2D.Force);
				break;
			case DirectionFacing.FACING_LEFT:
				transform.Rotate (0.0F, 0.0F, 180.0F);
				rb.AddForce (new Vector2(-speed, 0.0F), ForceMode2D.Force);
				break;
		}

		transform.Translate (0.25f, 0.0F, 0.0F);
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "SetGridArea" || other.tag == "FollowGridArea")
			Destroy (gameObject);
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "CombatCharacter") 
				other.gameObject.GetComponent<CombatCharacter> ().Freeze ();
	
		Destroy (gameObject);
	}
}