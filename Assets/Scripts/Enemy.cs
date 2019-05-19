using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 1;
    public int forwardSpeed = 0;
    public int turnSpeed = 0;
    public int ramDamage = 0;
    public int ramKnockback = 0;

    public float timeBetweenShots = 2;
    public BulletEnemy bulletEnemy;

    public Sprite spriteN;
    public Sprite spriteW;
    protected SpriteRenderer spriteRenderer;

    protected PlayerController player;
    private int currentHealth;
    protected Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Start is called before the first frame update
    void Start()
    {
        deactivate();

        player = Globals.player;

        // Set health to max
        currentHealth = maxHealth;

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void activate()
    {
        if (timeBetweenShots > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        enabled = true;
    }

    public void deactivate()
    {
        enabled = false;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    { 
        // https://wiki.unity3d.com/index.php/TorqueLookRotation
        Vector2 targetDelta = player.Position - body.position;

        // Turn towards player until they are facing them
        //get the angle between transform.up and target delta
        float angleDiff = Vector2.SignedAngle(transform.up, targetDelta);

        if (angleDiff > 1)
        {
            angleDiff = 1;
        } else if (angleDiff < -1)
        {
            angleDiff = -1;
        }

        // apply torque along that axis according to the magnitude of the angle.
        body.AddTorque(angleDiff * turnSpeed);
        RotateShip(body.rotation);

        // Add force to move forward
        body.AddForce(transform.up * forwardSpeed);
    }

    private void RotateShip(float rotation)
    {
        // Keep it between -180 and 180
        // If it is on the clockwise half of the circle, it should be negative
        rotation = rotation % 360;
        if (rotation > 180)
        {
            rotation -= 360;
        }
        else if (rotation < -180)
        {
            rotation += 360;
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collision if it is with a zone
        if (collision.gameObject.layer == 8)
        {
            return;
        }

        Vector2 collisionDirection = collision.ClosestPoint(body.position) - body.position;
        collisionDirection.Normalize();
        body.velocity = new Vector2(0, 0);
        body.AddForce(ramKnockback * -collisionDirection);

        PlayerController hitObject = collision.gameObject.GetComponent<PlayerController>();
        if (hitObject != null)
        {
            hitObject.OnHit(ramDamage);
            hitObject.body.AddForce(ramKnockback * collisionDirection);
        }
    }

    void Shoot()
    {
        Vector2 bulletDirection = player.Position - body.position;
        bulletDirection.Normalize();

        BulletEnemy newBullet = Instantiate(bulletEnemy, body.position, Quaternion.identity);
        newBullet.Direction = bulletDirection;

        if (enabled)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    public void OnHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void DestroyIfInactive()
    {
        if (!enabled)
        {
            Destroy(gameObject);
        }
    }
}
