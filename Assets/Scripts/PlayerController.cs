using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 10;
    public AudioClip damageSound;
    public Slider healthSlider;
    
    public int maxFuel = 10000;
    public Slider fuelSlider;
    
    public int maxBoost = 200;
    public Slider boostSlider;

    public UISurvivors survivorsUI;

    public float forwardSpeed;    // How fast the player can move forward
    public float turnSpeed;       // How fast the player can turn
    public BulletPlayer bulletPlayer;
    
    public Sprite spriteN;
    public Sprite spriteNW;
    public Sprite spriteW;
    public Sprite spriteSW;
    private SpriteRenderer spriteRenderer;

    public AudioSource movingSound;
    public AudioSource idleSound;

    public int currentZone;

    private int currentHealth;
    private int currentFuel;
    private int currentBoost;
    private bool survivorsOnBoard = false;

    private float waitTime = 1.0f;
    private float timer = 0.0f;

    public bool objectiveSet = false;
    private Vector3 objectiveLocation; 
    private GameObject pointerHolder;

    private AudioSource audioSource;
    public Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Vector2 Position
    {
        get
        {
            if (body != null) {
                return body.position;
            } else
            {
                return new Vector2(0, 0);
            }
        }
    }

    void Awake()
    {
        Globals.player = this;

        // Set health, fuel, and boost to max
        currentHealth = maxHealth;
        currentFuel = maxFuel;
        currentBoost = maxBoost;

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
        
        // Only boost if you have fuel, only replenish if not trying to use it
        if (Input.GetKey(KeyCode.LeftShift) && boostSlider.value > 0){
            forwardSpeed = 13.0f;
            currentBoost -= 4;
            boostSlider.value = currentBoost;
        } else {
            forwardSpeed = 3.0f;
        }
        
        if (currentBoost < maxBoost && !Input.GetKey(KeyCode.LeftShift))
        {
            currentBoost += 1;
            boostSlider.value = currentBoost;
        }
        
        // Fuel depletes when moving
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            currentFuel -= 1;
            fuelSlider.value = currentFuel;

            if (currentFuel <= 0)
            {
                // lose the game
                SceneManager.LoadScene("GameOverFuel", LoadSceneMode.Single);
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
        RotateShip(body.rotation);

        // Add force to move the player forward
        body.AddForce(transform.up * moveVertical * forwardSpeed);


        if (moveHorizontal == 0 & moveVertical == 0) {
            if (movingSound.isPlaying)
            {
                movingSound.Stop();
                idleSound.Play();
            }
        } else
        {
            if (!movingSound.isPlaying)
            {
                idleSound.Stop();
                movingSound.Play();
            }
        }
    }

    // Change ship sprite based on ship rotation
    private void RotateShip(float rotation)
    {
        // Keep it between -180 and 180
        // If it is on the clockwise half of the circle, it should be negative
        rotation = rotation % 360;
        if (rotation > 180)
        {
            rotation -= 360;
        } else if (rotation < -180)
        {
            rotation += 360;
        }

        int spriteIndex = (int) Mathf.Abs(Mathf.Round(rotation / 45f));
        switch(spriteIndex)
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
            default:
                spriteRenderer.sprite = spriteN;
                break;
        }

        // Flip sprite if rotation is negative
        spriteRenderer.flipX = rotation < 0;
    }

    public void OnHit(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(damageSound);

        if (currentHealth <= 0)
        {
            body.position = new Vector2(0, 0);
            currentHealth = maxHealth;
        }

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

    public void SetObjective(Vector3 location)
    {
        objectiveLocation = location;
        objectiveSet = true;

        survivorsUI.SetSurvivors(survivorsOnBoard, objectiveSet);
    }

    public Vector3 GetObjective()
    {
        return objectiveLocation;
    }

    // Pick up survivors from wreck
    public void RetrieveSurvivors()
    {
        survivorsOnBoard = true;
        survivorsUI.SetSurvivors(survivorsOnBoard, objectiveSet);

        SetObjective(Globals.homePort.transform.position);
    }

    // Bring survivors to port
    // Returns true if player was carrying survivor
    // else false
    public bool DepositSurvivors()
    {
        if (survivorsOnBoard)
        {
            // We not longer have survivors on board
            survivorsOnBoard = false;

            // Replenish supplies
            currentHealth = maxHealth;
            currentFuel = maxFuel;
            currentBoost = maxBoost;

            // No objective for now
            objectiveSet = false;
            survivorsUI.SetSurvivors(survivorsOnBoard, objectiveSet);

            // Spawn a new wreck
            Globals.wreckSpawner.SpawnWreck();

            // Respawn monsters if they are inactive
            Globals.monsterZoneManager.RespawnMonsters();

            return true;
        } 

        return false;
    }
}
