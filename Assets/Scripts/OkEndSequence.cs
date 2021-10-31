using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkEndSequence : MonoBehaviour
{
    private const float startTime = 2.5f;
    private const float waitTimeDestroy = 0.75f;
    private const float waitTimeEnd = 2f;

    public Sprite castleDestroyed1;
    public Sprite castleDestroyed2;
    public Sprite castleDestroyed3;
    public Sprite castleDestroyed4;
    public Sprite castleDestroyed5;
    public AudioClip destroySound;
    public GameObject endObj;

    private SpriteRenderer sr;
    private AudioSource audioSource;
    private CameraShake cameraShake;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(startTime);
        sr.sprite = castleDestroyed1;
        audioSource.PlayOneShot(destroySound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeDestroy);
        sr.sprite = castleDestroyed2;
        audioSource.PlayOneShot(destroySound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeDestroy);
        sr.sprite = castleDestroyed3;
        audioSource.PlayOneShot(destroySound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeDestroy);
        sr.sprite = castleDestroyed4;
        audioSource.PlayOneShot(destroySound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeDestroy);
        sr.sprite = castleDestroyed5;
        audioSource.PlayOneShot(destroySound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeEnd);
        endObj.SetActive(true);
    }
}
