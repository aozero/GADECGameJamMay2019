using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController hitObject = collision.gameObject.GetComponent<PlayerController>();

        if (hitObject != null)
        {
            hitObject.OnHit(damage);

            PlaySoundAndDestroy();
        }
    }
}
