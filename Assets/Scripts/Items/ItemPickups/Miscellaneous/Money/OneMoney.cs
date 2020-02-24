using UnityEngine;
using System.Collections;

public sealed class OneMoney : Money 
{
	protected override void Start () {
		base.Start ();
		moneyValue = 1;
	}
}
