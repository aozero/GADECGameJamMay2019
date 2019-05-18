using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 1;

    public PlayerController player;
    public float timeBetweenShots = 2;
    public BulletEnemy bulletEnemy;

    private int currentHealth;
    private Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max
        currentHealth = maxHealth;

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
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
