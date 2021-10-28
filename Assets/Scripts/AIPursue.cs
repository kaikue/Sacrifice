using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPursue : AIMovement
{
    public bool canJump = false;
    public float jumpForce = 8;

    protected override Vector2 GetVelocity()
    {
        float dir = facingLeft ? -1 : 1;
        float yVel = rb.velocity.y;
        if (canJump && onGround && ((wallLeft && facingLeft) || (wallRight && !facingLeft)))
        {
            yVel = jumpForce;
		}
        return new Vector2(speed * dir, yVel);
    }
}
