using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPursue : MonoBehaviour
{
    private const float minXDist = 0.5f;
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
        float xDiff = Mathf.Abs(player.transform.position.x - rb.position.x);
        float xVel = 0;
        if (xDiff <= activationDist)
		{
            if (xDiff > minXDist)
            {
                facingLeft = player.transform.position.x < rb.position.x;
            }
            sr.flipX = facingLeft;

            float dir = facingLeft ? -1 : 1;
            xVel = speed * dir;
        }
        
        Vector2 vel = new Vector2(xVel, rb.velocity.y);
        rb.velocity = vel;
        //rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
    }
}
