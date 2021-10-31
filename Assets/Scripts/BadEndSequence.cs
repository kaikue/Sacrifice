using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndSequence : MonoBehaviour
{
    private const float startTime = 2.5f;
    private const float waitTime1 = 1;
    private const float waitTime2 = 0.75f;
    private const float waitTime3 = 0.75f;
    private const float waitTimeEnd = 2f;

    public Sprite castleDestroyed1;
    public Sprite castleDestroyed2;
    public Sprite castleDestroyed2Eye;
    public AudioClip tentacleBurstSound;
    public GameObject tentaclePrefab;
    public Transform tentaclePoint1;
    public Transform tentaclePoint2;
    public Transform tentaclePoint3;
    public Transform tentaclePoint4;
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
        Instantiate(tentaclePrefab, tentaclePoint1);
        audioSource.PlayOneShot(tentacleBurstSound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTime1);
        sr.sprite = castleDestroyed2;
        Instantiate(tentaclePrefab, tentaclePoint2);
        audioSource.PlayOneShot(tentacleBurstSound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTime2);
        Instantiate(tentaclePrefab, tentaclePoint3);
        audioSource.PlayOneShot(tentacleBurstSound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTime3);
        Instantiate(tentaclePrefab, tentaclePoint4);
        audioSource.PlayOneShot(tentacleBurstSound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTime3);
        sr.sprite = castleDestroyed2Eye;
        audioSource.PlayOneShot(tentacleBurstSound);
        cameraShake.Shake();
        yield return new WaitForSeconds(waitTimeEnd);
        endObj.SetActive(true);
        audioSource.PlayOneShot(tentacleBurstSound);
    }
}
