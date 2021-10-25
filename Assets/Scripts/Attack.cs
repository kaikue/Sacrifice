using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private const float decayTime = 0.1f;

    public void Activate()
    {
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
	{
        yield return new WaitForSeconds(decayTime);
        gameObject.SetActive(false);
	}
}
