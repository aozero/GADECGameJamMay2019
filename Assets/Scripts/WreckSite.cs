using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckSite : MonoBehaviour
{
    public AudioClip pickupSound;

    private bool survivorsWaiting = true;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (survivorsWaiting)
        {
            PlayerController hitObject = collision.gameObject.GetComponent<PlayerController>();

            if (hitObject != null)
            {
                hitObject.RetrieveSurvivors();
                audioSource.PlayOneShot(pickupSound);

                survivorsWaiting = false;
                spriteRenderer.color = Color.black;
            }
        }
    }
}
