using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOrbDash : ChoiceOrb
{
	public override void Activate()
	{
		persistent.sacrificedDash = true;
		base.Activate();
	}
}
