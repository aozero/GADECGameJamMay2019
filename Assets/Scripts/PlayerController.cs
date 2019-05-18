using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed;    // How fast the player can move forward
    public float turnSpeed;       // How fast the player can turn
    public PlayerBullet playerBullet;

    private Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fire a bullet towards the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bulletDirection = new Vector2(mousePos[0], mousePos[1]) - body.position;
            bulletDirection.Normalize();

            PlayerBullet newBullet = Instantiate(playerBullet, body.position, Quaternion.identity);
            newBullet.Direction = bulletDirection;
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // Store movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Use the vertical movement to set the forward speed
        Vector2 movement = new Vector2(0, moveVertical);

        // Add torque to turn the player
        body.AddTorque(-moveHorizontal * turnSpeed);
   
        // Add force to move the player forward
        body.AddForce(transform.up * moveVertical * forwardSpeed);
    }
}
