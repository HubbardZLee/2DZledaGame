using UnityEngine;
using System.Collections;

public sealed class FiveMoney : Money 
{
	protected override void Start () {
		moneyValue = 5;
		base.Start ();
	}
}