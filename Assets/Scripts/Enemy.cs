using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float hurtInvincibleTime = 0.4f;

    private bool hurtInvincible = false;
    private float hurtInvincibleTimer;
    protected int health;

    private AIMovement ai;

    public int hits;
    public float knockbackResist = 1;
    public float heartDropChance = 0.3f;
    public GameObject heartPrefab;
    public GameObject killParticlePrefab;
    public GameObject hurtParticlePrefab;

    protected virtual void Start()
    {
        health = hits;
        ai = GetComponent<AIMovement>();
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
        health -= hearts;
        if (health <= 0)
		{
            Player player = FindObjectOfType<Player>();
            if (player.GetHearts() < player.GetMaxHearts() && Random.Range(0f, 1f) < heartDropChance)
			{
                Instantiate(heartPrefab, transform.position, Quaternion.identity);
            }
            Instantiate(killParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
        }
	}
}
