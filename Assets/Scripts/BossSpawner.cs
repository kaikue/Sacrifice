using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private const float flashTime = 0.1f;
    private float flashTimer;
    private SpriteRenderer sr;
    public Sprite activeSprite;
    private Sprite normalSprite;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;
    }

	private void Update()
	{
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                flashTimer = 0;
                sr.sprite = normalSprite;
            }
        }
	}

	public void Spawn(GameObject enemy)
	{
        Instantiate(enemy, transform.position, Quaternion.identity);
        sr.sprite = activeSprite;
        flashTimer = flashTime;
	}
}
