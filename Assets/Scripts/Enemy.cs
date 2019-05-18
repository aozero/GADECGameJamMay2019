using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController player;
    public float timeBetweenShots = 2;
    public BulletEnemy bulletEnemy;

    private Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        body = GetComponent<Rigidbody2D>();

        Invoke("Shoot", timeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void Shoot()
    {
        Vector2 bulletDirection = player.Position - body.position;
        bulletDirection.Normalize();

        BulletEnemy newBullet = Instantiate(bulletEnemy, body.position, Quaternion.identity);
        newBullet.Direction = bulletDirection;

        Invoke("Shoot", timeBetweenShots);
    }

    public void OnHit(int damage)
    {
        Destroy(gameObject);
    }
}
