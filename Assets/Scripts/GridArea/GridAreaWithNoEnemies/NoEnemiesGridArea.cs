using UnityEngine;
using System.Collections;

public class NoEnemiesGridArea : GridArea
{
	protected override void Start() {
		base.Start ();

		isSaveScreen = true;
	}
}
