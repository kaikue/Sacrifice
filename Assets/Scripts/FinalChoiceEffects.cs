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
    private float rumbleTimer = 0;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void StartRumbling(float rumbleTimer = 0)
	{
        this.rumbleTimer = rumbleTimer;
        StartCoroutine(Rumble());
	}

    public void FadeToWhite(string endScene)
	{
        FindObjectOfType<Player>().SetInvincible();
        StartCoroutine(Fade(endScene));
	}

    private IEnumerator Rumble()
    {
        while (true)
        {
            cameraShake.Shake();
            yield return new WaitForSeconds(rumbleTime);
            if (rumbleTimer > 0)
			{
                rumbleTimer -= rumbleTime;
                if (rumbleTimer <= 0)
				{
                    rumbleTimer = 0;
                    break;
				}
			}
        }
    }

    private IEnumerator Fade(string endScene)
	{
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
		{
            fader.color = new Color(1, 1, 1, t / fadeTime);
            yield return null;
		}
        yield return new WaitForSeconds(holdWhiteTime);
        SceneManager.LoadScene(endScene);
    }
}
