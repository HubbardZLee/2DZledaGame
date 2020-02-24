using UnityEngine;
using System.Collections;

public sealed class TwentyMoney : Money 
{
	protected override void Start () {
		base.Start ();
		moneyValue = 20;
	}
}