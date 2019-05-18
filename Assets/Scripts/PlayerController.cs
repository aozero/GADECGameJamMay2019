﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 10;
    public Slider healthSlider;
    
    public int maxBoost = 200;
    public Slider boostSlider;

    public float forwardSpeed;    // How fast the player can move forward
    public float turnSpeed;       // How fast the player can turn
    public BulletPlayer bulletPlayer;

    private int currentHealth;
    private int currentBoost;
    
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    
    private Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Vector2 Position
    {
        get
        {
            return body.position;
        }
    }

    void Awake()
    {
        // Set health and boost to max
        currentHealth = maxHealth;
        currentBoost = maxBoost;

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0) && timer > waitTime)
        {
            // Fire a bullet towards the mouse
            Fire();
            timer = 0.0f;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && boostSlider.value > 0){
            currentBoost -= 4;
            boostSlider.value = currentBoost;
        }
        if (currentBoost < maxBoost && !Input.GetKey(KeyCode.LeftShift))
        {
            currentBoost += 1;
            boostSlider.value = currentBoost;
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

    public void OnHit(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
    }
    
    // Fire a bullet towards the mouse
    void Fire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bulletDirection = new Vector2(mousePos[0], mousePos[1]) - body.position;
        bulletDirection.Normalize();

        BulletPlayer newBullet = Instantiate(bulletPlayer, body.position, Quaternion.identity);
        newBullet.Direction = bulletDirection;
    }
}
