using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOrbHearts : ChoiceOrb
{
	public override void Activate()
	{
		persistent.sacrificedHearts = true;
		FindObjectOfType<HUD>().RefreshHearts();
		base.Activate();
	}
}
