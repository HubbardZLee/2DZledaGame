using UnityEngine;
using System.Collections;

public class AttackPowerRing : PowerRing 
{
	protected override void ringPutOn () {
		_gameManager.Player.PlayerAttackMultiplier++;
	}

	protected override void ringTakenOff () {
		_gameManager.Player.PlayerAttackMultiplier--;
	}

}
