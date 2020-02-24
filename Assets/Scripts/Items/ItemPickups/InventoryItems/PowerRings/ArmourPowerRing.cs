using UnityEngine;
using System.Collections;

public class ArmourPowerRing : PowerRing 
{
	protected override void ringPutOn () {
		_gameManager.Player.PlayerArmourMultiplier++;
	}

	protected override void ringTakenOff () {
		_gameManager.Player.PlayerArmourMultiplier--;
	}

}
