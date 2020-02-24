using UnityEngine;
using System.Collections;

public sealed class FiftyMoney : Money 
{
	protected override void Start () {
		base.Start ();

		moneyValue = 50;
	}
}