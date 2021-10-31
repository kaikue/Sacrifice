using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEndSequence : MonoBehaviour
{
    public SpriteRenderer colorCastle;
    public GameObject endControls;

    private const float waitTime = 2f;
    private const float fadeTime = 5f;
    private const float waitTime2 = 3f;

    private void Start()
    {
        colorCastle.color = new Color(1, 1, 1, 0);
        StartCoroutine(EndSeq());
    }

    private IEnumerator EndSeq()
	{
        yield return new WaitForSeconds(waitTime);
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
		{
            colorCastle.color = new Color(1, 1, 1, t / fadeTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime2);
        endControls.SetActive(true);
	}
}
