using UnityEngine;
using System.Collections;

public sealed class OneHundredMoney : Money 
{
	protected override void Start () {
		base.Start ();
		moneyValue = 100;
	}
}