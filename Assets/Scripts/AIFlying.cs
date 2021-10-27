using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlying : MonoBehaviour
{
    private const float minDist = 0.5f;
    private const float activationDist = 10;

    public bool facingLeft;
    public float speed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject player;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().gameObject;
    }

    private void FixedUpdate()
    {
        float dist = Vector2.Distance(player.transform.position, rb.position);
        Vector2 vel = Vector2.zero;
        if (dist <= activationDist)
        {
            if (dist > minDist)
            {
                facingLeft = player.transform.position.x < rb.position.x;
            }
            sr.flipX = facingLeft;

            Vector2 diff = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
            vel = diff.normalized * speed;
        }
        rb.velocity = vel;
        rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
    }
}
