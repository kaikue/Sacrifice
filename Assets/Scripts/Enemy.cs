using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float hurtInvincibleTime = 0.4f;

    private bool hurtInvincible = false;
    private float hurtInvincibleTimer;
    private int health;

    private AIMovement ai;

    public int hits;
    public float knockbackResist = 1;

    private void Start()
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
        //TODO effects
        health -= hearts;
        if (health <= 0)
		{
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
		}
	}
}
