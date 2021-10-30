using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour
{
    [HideInInspector]
    public bool destroying = false;
    [HideInInspector]
    public int gems = 0;
    [HideInInspector]
    public float time = 0;

    public bool sacrificedDash = false;
    public bool sacrificedDoubleJump = false;
    public bool sacrificedHearts = false;

    private const float musicFadeTime = 2;
    private const float musicEndTime = 1;
    private AudioSource audioSource;

    private Player player;

    private void Awake()
    {
        Persistent[] persistents = FindObjectsOfType<Persistent>();

        if (persistents.Length > 1)
        {
            destroying = true;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        audioSource = GetComponent<AudioSource>();
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        int levelGems = FindObjectsOfType<Gem>().Length;
        player = FindObjectOfType<Player>();

        BGMHolder bgm = FindObjectOfType<BGMHolder>();
        if (bgm != null)
		{
            AudioClip music = bgm.music;
            if (audioSource.clip == null)
			{
                audioSource.clip = music;
                audioSource.Play();
			}
            else if (audioSource.clip != music)
			{
                StartCoroutine(FadeMusic(music));
			}
        }
    }

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            time += Time.deltaTime;
        }
    }

    public int NumSacrifices()
	{
        int n = 0;
        if (sacrificedDash) n++;
        if (sacrificedDoubleJump) n++;
        if (sacrificedHearts) n++;
        return n;
    }

    private IEnumerator FadeMusic(AudioClip music)
	{
        float startVol = audioSource.volume;
        for (float t = 0; t < musicFadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVol, 0, t / musicFadeTime);
            yield return null;
        }
        yield return new WaitForSeconds(musicEndTime);

        audioSource.volume = startVol;
        audioSource.clip = music;
        audioSource.Play();
    }

    public int GetTotalGems()
	{
        return gems + (player ? player.levelGems : 0);
	}
}
