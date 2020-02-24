using UnityEngine;
using System.Collections;

public sealed class TenMoney : Money 
{
	protected override void Start () {
		base.Start ();
		moneyValue = 10;
	}
}