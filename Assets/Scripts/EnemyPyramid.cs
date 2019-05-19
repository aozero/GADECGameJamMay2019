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

        int spriteIndex = (int)Mathf.Abs(Mathf.Round(rotation / 45f));
        switch (spriteIndex)
        {
            case 1:
                spriteRenderer.sprite = spriteNW;
                break;
            case 2:
                spriteRenderer.sprite = spriteW;
                break;
            case 3:
                spriteRenderer.sprite = spriteSW;
                break;
            case 4:
                spriteRenderer.sprite = spriteS;
                break;
            default:
                spriteRenderer.sprite = spriteN;
                break;
        }

        // Flip sprite if rotation is negative
        spriteRenderer.flipX = rotation < 0;
    }
}
