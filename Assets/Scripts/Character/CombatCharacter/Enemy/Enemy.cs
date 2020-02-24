using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Abstract class to derive every enemy type from
 */
public abstract class Enemy : CombatCharacter
{
	// Destroys the enemy if it leaves the current screen
	protected virtual void OnTriggerExit2D(Collider2D other) {
		if (other == currentArea)
			Destroy (gameObject);
	}
}