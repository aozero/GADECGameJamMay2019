using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy hitObject = collision.gameObject.GetComponent<Enemy>();

        if (hitObject != null)
        {
            hitObject.OnHit();

            Destroy(gameObject);
        }
    }
}
