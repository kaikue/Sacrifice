using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hits;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        Attack attack = collider.GetComponent<Attack>();
        if (attack != null)
        {
            /*if (!hurtInvincible)
            {
                Damage();
            }*/
            Destroy(gameObject); //TODO
        }
    }
}
