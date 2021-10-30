using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalChoiceEffects : MonoBehaviour
{
    private CameraShake cameraShake;
    private const float rumbleTime = 0.2f;
    private const float fadeTime = 3.0f;
    private const float holdWhiteTime = 2.0f;
    public Image fader;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        FindObjectOfType<Player>().DisableAttack();
    }

    public void StartRumbling()
	{
        StartCoroutine(Rumble());
	}

    public void FadeToWhite()
	{
        StartCoroutine(Fade());
	}

    private IEnumerator Rumble()
    {
        while (true)
        {
            cameraShake.Shake();
            yield return new WaitForSeconds(rumbleTime);
        }
    }

    private IEnumerator Fade()
	{
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
		{
            fader.color = new Color(1, 1, 1, t / fadeTime);
            yield return null;
		}
        yield return new WaitForSeconds(holdWhiteTime);
        SceneManager.LoadScene("BadEnd");
    }
}
