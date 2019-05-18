using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPyramid : Enemy
{
    private void FixedUpdate()
    {
        RotateShip();
    }

    // Rotate pyramid to always be facing the player
    private void RotateShip()
    {
        Vector2 targetDelta = player.Position - body.position;

        float rotation = Vector2.SignedAngle(transform.up, targetDelta);

        int spriteIndex = (int)Mathf.Abs(Mathf.Round(rotation / 90f));
        switch (spriteIndex)
        {
            case 1:
                spriteRenderer.sprite = spriteW;
                break;
            default:
                spriteRenderer.sprite = spriteN;
                break;
        }

        // Flip sprite if rotation is negative
        spriteRenderer.flipX = rotation < 0;
    }
}
