using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePort : MonoBehaviour
{
    public AudioClip arrivePortSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Globals.homePort = this;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController hitObject = collision.gameObject.GetComponent<PlayerController>();

        if (hitObject != null)
        {
            if (hitObject.DepositSurvivors())
            {
                // Successful return
                audioSource.PlayOneShot(arrivePortSound);
            }
        }
    }
}
