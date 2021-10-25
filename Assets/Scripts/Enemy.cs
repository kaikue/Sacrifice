using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float hurtInvincibleTime = 0.2f;

    private bool hurtInvincible = false;
    private float hurtInvincibleTimer;
    private int health;

    public int hits;

    private void Start()
    {
        health = hits;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        Attack attack = collider.GetComponent<Attack>();
        if (attack != null)
        {
            if (!hurtInvincible)
            {
                Damage();
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

	private void Damage()
	{
        //TODO effects
        health--;
        if (health <= 0)
		{
            Destroy(gameObject);
        }
        else
		{
            hurtInvincible = true;
            hurtInvincibleTimer = 0;
		}
	}
}
