using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
	public void Destroy()
	{
		FindObjectOfType<BossController>().DestroyEye();
	}
}
