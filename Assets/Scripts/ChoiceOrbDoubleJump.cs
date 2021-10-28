using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOrbDoubleJump : ChoiceOrb
{
	public override void Activate()
	{
		persistent.sacrificedDoubleJump = true;
		base.Activate();
	}
}
