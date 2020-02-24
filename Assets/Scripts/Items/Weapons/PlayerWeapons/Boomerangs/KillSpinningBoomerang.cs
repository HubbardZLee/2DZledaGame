using UnityEngine;
using System.Collections;

/*
 * This is the component that will be placed on the boomerang prefab.
 * It will fly in a circle while spinning, hitting any enemies along its path.
 * It will hurt and possibly kill any enemies in its way and pickup up items too.
 */
public sealed class KillSpinningBoomerang : Weapon 
{
	// The sprites used to simulate the animation
	[SerializeField]
	Sprite [] animations;

	// Holds the objects sprite image to change later without constantly calling getComponent
	SpriteRenderer spriteRend;

	// The amount of time between sprite changes
	float animationTime = 0.15F;

	// How quickly the boomerang flys
	float speed = 5.0F;

	// Rotation speed creates the size of the circular path
	float rotateSpeed = 255.0F;

	// Prevents the boomerang from picking up more than one item.
	bool itemHasBeenGrabbed = false;

	// Has the player thrown the boomerang yet. Makes sure that the boomerang won't destroy on contact until it should
	bool playerHasThrown = false;

	// Initialization code to go here
	protected override void Start () {
		base.Start ();

		spriteRend = GetComponent<SpriteRenderer> ();

		attackDamage = 1;

		StartCoroutine ("Animate", animationTime);
	}

	// Causes the boomerang to fly in a circular path depending on which way the player is facing
	void Update () {
		switch (directionFacing) {
			case DirectionFacing.FACING_UP:
				gameObject.transform.Translate (Time.deltaTime * speed, 0.0F, 0.0F);
				break;
			case DirectionFacing.FACING_RIGHT:
				gameObject.transform.Translate (0.0F, -Time.deltaTime * speed, 0.0F);
				break;
			case DirectionFacing.FACING_DOWN:
				gameObject.transform.Translate (-Time.deltaTime * speed, 0.0F, 0.0F);
				break;
			case DirectionFacing.FACING_LEFT:
				gameObject.transform.Translate (0.0F, Time.deltaTime * speed, 0.0F);
				break;
		}
			
		transform.Rotate (0.0F, 0.0F, rotateSpeed * Time.deltaTime);
	}

	// The boomerang will have different behaviours depending on what it hits.
	void OnTriggerEnter2D (Collider2D col) {
		switch (col.tag) {
			case "ItemPickup":
				if (!itemHasBeenGrabbed && col.GetComponent<ItemPickup> ().IsSnatchable) {
					col.transform.parent = transform;
					col.transform.localPosition = Vector2.zero;
				}
				break;

			case "Player":
				if (playerHasThrown)
					Destroy (gameObject);
				break;

			case "CombatCharacter":
				Attack (col.gameObject);
				break;
		}  // End switch

		if (col.gameObject.GetType() == typeof(CombatCharacter))
			Attack (col.gameObject);
	}  // End OnTriggerEnter2D


	// Prevents the boomerang from thinking it's being caught when first instantiated
	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Player")
			playerHasThrown = true;
	}

	// The animation of the boomerang
	IEnumerator Animate () {
		int i = 0;

		while (true) {
			if (i >= animations.Length)
				i = 0;

			spriteRend.sprite = animations [i];
			yield return new WaitForSeconds (0.1F);

			i++;
		}
	}
}
