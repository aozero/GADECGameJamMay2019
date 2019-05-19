using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;    // Speed bullet travels
    public int duration = 5;
    public int damage = 1;

    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip hitSound;

    private Vector2 direction; // Direction bullet is going

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(fireSound);

        // Bullet will be destroyed after duration seconds
        Destroy(gameObject, duration);
    }

    public void PlaySoundAndDestroy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        audioSource.PlayOneShot(hitSound);

        Destroy(gameObject, 2);
    }
}
