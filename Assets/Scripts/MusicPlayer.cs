using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip homeMusic;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.loop = true;
        musicSource.clip = homeMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
