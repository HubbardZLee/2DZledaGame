using UnityEngine;
using System.Collections;

public abstract class Weapon : Item 
{
	// Determines which way the weapon will go
	protected DirectionFacing directionFacing;

	// The amount of damage that this weapon will do
	[SerializeField]
	protected int attackDamage;
	public int AttackDamage { get { return attackDamage; } }

	protected override void Start () {
		base.Start ();

		directionFacing = _gameManager.Player.directionFacing;
	}

	protected virtual void Attack (GameObject victim) {
		attackDamage += _gameManager.Player.PlayerAttackMultiplier;

		victim.GetComponent<CombatCharacter>().TakeDamage (this);
	}
}
