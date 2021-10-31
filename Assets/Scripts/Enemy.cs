using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private const float hurtInvincibleTime = 0.4f;

    private bool hurtInvincible = false;
    private float hurtInvincibleTimer;
    protected int health;

    private AIMovement ai;
    private CameraShake cameraShake;
    private bool destroyed = false;

    public int hits;
    public float knockbackResist = 1;
    public float heartDropChance = 0.3f;
    public AudioClip hurtSound;
    public AudioClip killSound;
    public GameObject heartPrefab;
    public GameObject killParticlePrefab;
    public GameObject hurtParticlePrefab;
    public GameObject soundEffectPrefab;
    public UnityEvent killEvent;

    protected virtual void Start()
    {
        health = hits;
        ai = GetComponent<AIMovement>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        Attack attack = collider.GetComponent<Attack>();
        if (attack != null)
        {
            if (!hurtInvincible)
            {
                Damage(attack.hearts, attack.knockback);
            }
        }
    }

	private void FixedUpdate()
	{
		if (hurtInvincible)
		{
            hurtInvincibleTimer += Time.fixedDeltaTime;
            if (hurtInvincibleTimer >= hurtInvincibleTime)
			{
                hurtInvincible = false;
			}
		}
	}

	private void Damage(int hearts, float knockback)
	{
        if (destroyed) return;

        health -= hearts;

        if (health <= 0)
		{
            Player player = FindObjectOfType<Player>();
            if (player.GetHearts() < player.GetMaxHearts() && Random.Range(0f, 1f) < heartDropChance)
			{
                Instantiate(heartPrefab, transform.position, Quaternion.identity);
            }
            Instantiate(killParticlePrefab, transform.position, Quaternion.identity);
            PlaySound(killSound);
            cameraShake.Shake(0.75f);
            killEvent.Invoke();
            Destroy(gameObject);
            destroyed = true;
        }
        else
		{
            hurtInvincible = true;
            hurtInvincibleTimer = 0;
            if (ai != null)
            {
                ai.Knockback(knockback / knockbackResist);
            }
            Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
            PlaySound(hurtSound);
            cameraShake.Shake(0.5f);
        }
	}

    private void PlaySound(AudioClip clip, float volume = 1)
	{
        AudioSource src = Instantiate(soundEffectPrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = volume;
        src.Play();
    }
}
