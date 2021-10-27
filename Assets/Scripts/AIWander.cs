using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWander : MonoBehaviour
{
    public bool facingLeft;
    public float speed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //TODO check for empty at feet, or wall at face: if so, flip direction
        sr.flipX = facingLeft;

        float dir = facingLeft ? -1 : 1;
        float xVel = speed * dir;
        Vector2 vel = new Vector2(xVel, rb.velocity.y);
        rb.velocity = vel;
        //rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
    }
}
