using UnityEngine;
using System.Collections;

public class ThrowingBomb : Weapon
{
	[SerializeField]
	Sprite [] animations;
	SpriteRenderer spriteRend;

	// How long the bombs fuse is
	[SerializeField]
	private float countdownTimer = 3.0F;
	public float CountdownTimer { get { return countdownTimer; } }

	[ReadOnly][SerializeField]
	bool countdownStarted = false;

	// How far the bomb will be thrown
	[SerializeField]
	private float throwingDistance;
	public float ThrowingDistance { get { return throwingDistance; } }

	[ReadOnly][SerializeField]
	DirectionFacing thisObjectFacing;
	[ReadOnly][SerializeField]
	Vector3 startingPosition;

	// How fast the bomb will be thrown
	[SerializeField]
	private float throwingSpeed;
	public float ThrowingSpeed { get { return throwingSpeed; } }

	bool exploded;

	// The blast radius
	[SerializeField]
	private float blastRadius;
	public float BlastRadius { get { return blastRadius; } }

	protected override void Start () {
		base.Start ();

		spriteRend = GetComponent<SpriteRenderer> ();

		attackDamage = 2;
		blastRadius = 4.5F;
		startingPosition = transform.position;
		thisObjectFacing = _gameManager.Player.directionFacing;
	}

	// Update is called once per frame
	void Update () {

		if (!countdownStarted) {
			switch (thisObjectFacing) {
				case DirectionFacing.FACING_UP:
					transform.Translate (0.0F, Time.deltaTime * ThrowingSpeed, 0.0F);
					break;
				case DirectionFacing.FACING_RIGHT:
					transform.Translate (Time.deltaTime * ThrowingSpeed, 0.0F, 0.0F);
					break;
				case DirectionFacing.FACING_DOWN:
					transform.Translate (0.0F, -Time.deltaTime * ThrowingSpeed, 0.0F);
					break;
				case DirectionFacing.FACING_LEFT:
					transform.Translate (-Time.deltaTime * ThrowingSpeed, 0.0F, 0.0F);
					break;
			}
		} 

		if ((transform.position - startingPosition).magnitude > ThrowingDistance && !countdownStarted)
			StartCoroutine ("StartCountdown");
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "CombatCharacter" || col.tag == "Player")
			Attack (col.gameObject);
	}

	IEnumerator StartCountdown () { 
		countdownStarted = true;
		StartCoroutine ("CountdownAnimation");
		yield return new WaitForSeconds (countdownTimer);
		StartCoroutine ("Explode");
	}

	IEnumerator CountdownAnimation () {
		int count = 1;
		int increment = 1;

		while (!exploded) {
			if (count < 1 || count > 5) {
				increment = -increment;
				count += increment;
			}

			spriteRend.sprite = animations [count];
			yield return new WaitForSeconds (0.1F);

			count += increment;
		}

		spriteRend.sprite = animations [0];
	}

	IEnumerator Explode () {
		exploded = true;
		CircleCollider2D blastZone = gameObject.AddComponent<CircleCollider2D> ();
		blastZone.isTrigger = true;
		blastZone.radius *= blastRadius;
		yield return new WaitForSeconds (0.2F);
		Destroy (gameObject);
	}
}
