using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    protected const float minDist = 0.5f;
    protected const float activationDist = 10;
    protected const float knockbackFloorDecay = 0.9f;
    protected const float knockbackFlyingDecay = 0.95f;

    public bool facingLeft;
    public float speed = 5;

    protected Rigidbody2D rb;
    protected EdgeCollider2D ec;
    protected SpriteRenderer sr;
    protected GameObject player;

    protected float knockback = 0;
    protected bool knockbackLeft;
    protected bool flying;
    protected bool onGround;
    protected bool wallLeft;
    protected bool wallRight;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        ec = gameObject.GetComponent<EdgeCollider2D>();
        player = FindObjectOfType<Player>().gameObject;
        flying = rb.gravityScale == 0;
    }

    protected bool CheckPoint(int ecPoint, Vector2 direction)
    {
        Vector2 point = rb.position + ec.points[ecPoint] + direction * 0.02f;
        return Physics2D.OverlapPoint(point, LayerMask.GetMask("Tiles")) != null;
    }

    protected Collider2D RaycastTiles(Vector2 startPoint, Vector2 endPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, endPoint - startPoint, Vector2.Distance(startPoint, endPoint), LayerMask.GetMask("Tiles"));
        return hit.collider;
    }

    protected bool CheckSide(int point0, int point1, Vector2 direction)
    {
        Vector2 startPoint = rb.position + ec.points[point0] + direction * 0.02f;
        Vector2 endPoint = rb.position + ec.points[point1] + direction * 0.02f;
        Collider2D collider = RaycastTiles(startPoint, endPoint);
        return collider != null;
    }

    protected virtual Vector2 GetVelocity()
	{
        return rb.velocity;
	}

    protected virtual void UpdateDirection()
	{
        float xDiff = Mathf.Abs(player.transform.position.x - rb.position.x);
        if (xDiff > minDist)
        {
            facingLeft = player.transform.position.x < rb.position.x;
        }
    }

    private void FixedUpdate()
    {
        onGround = CheckSide(4, 3, Vector2.down);
        wallLeft = CheckSide(1, 0, new Vector2(-1, 3));
        wallRight = CheckSide(2, 3, new Vector2(1, 3));

        float dist = Vector2.Distance(player.transform.position, rb.position);
        Vector2 vel = Vector2.zero;
        if (knockback > 0)
		{
            if (onGround)
            {
                knockback *= knockbackFloorDecay;
            }
            if (flying)
			{
                knockback *= knockbackFlyingDecay;
            }
            if (knockback <= 0.1f)
			{
                knockback = 0;
			}

            if ((wallLeft && knockbackLeft) || (wallRight && !knockbackLeft))
			{
                knockback = 0;
			}

            float knockbackX = knockback * (knockbackLeft ? -1 : 1);
            float knockbackY;
            if (flying)
			{
                knockbackY = knockback;
            }
            else
			{
                knockbackY = rb.velocity.y;
			}
            vel = new Vector2(knockbackX, knockbackY);
        }
        else
		{
            if (dist <= activationDist)
            {
                UpdateDirection();
                sr.flipX = facingLeft;
                vel = GetVelocity();
            }
        }
        rb.velocity = vel;
    }

    public void Knockback(float force)
	{
        if (rb.gravityScale > 0)
        {
            rb.velocity = new Vector2(0, force);
        }
        knockback = force;
        knockbackLeft = player.transform.position.x > rb.position.x;
	}
}
