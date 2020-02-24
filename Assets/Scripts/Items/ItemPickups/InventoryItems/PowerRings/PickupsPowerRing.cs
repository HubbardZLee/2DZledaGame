using UnityEngine;
using System.Collections;

public class PickupsPowerRing : PowerRing 
{
	protected override void ringPutOn () {
		_gameManager.Player.PlayerPickupMultiplier += 10;
	}

	protected override void ringTakenOff () {
		_gameManager.Player.PlayerPickupMultiplier -= 10;
	}

}
