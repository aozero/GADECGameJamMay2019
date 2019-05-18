using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;    // Speed bullet travels
    public int duration;

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
        // Bullet will be destroyed after duration seconds
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
