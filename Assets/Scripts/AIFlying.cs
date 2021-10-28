using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlying : AIMovement
{
    protected override Vector2 GetVelocity()
    {
        Vector2 diff = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        return diff.normalized * speed;
    }
}
