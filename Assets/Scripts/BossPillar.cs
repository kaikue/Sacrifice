using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPillar : MonoBehaviour
{
	public void Destroy()
	{
		FindObjectOfType<BossController>().DestroyPillar();
	}
}
