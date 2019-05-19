using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip song0;

    public AudioClip song0A;
    public AudioClip song10;
    public AudioClip song11;
    public AudioClip song12;
    public AudioClip song13;

    public AudioClip song0B;
    public AudioClip song20;
    public AudioClip song21;
    public AudioClip song22;
    public AudioClip song23;

    public AudioSource player1;
    public AudioSource player2;

    private List<AudioClip> order0 = new List<AudioClip>();
    private List<AudioClip> order1 = new List<AudioClip>();

    private int counter = 0;
    private int current_player = 1;
    private int current_zone = 0;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = Globals.player;

        order0.Add(song10);
        order0.Add(song11);
        order0.Add(song12);
        order0.Add(song13);

        order1.Add(song20);
        order1.Add(song21);
        order1.Add(song22);
        order1.Add(song23);

        player1.PlayOneShot(song0);
        current_player = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.isPlaying && !player2.isPlaying)
        {
            int playerZone = playerController.currentZone;
            if (playerZone > 0) { playerZone = 1; }

            if (current_zone > playerZone)
            {
                // Transition to zone 1
                PlaySong(song0A);
                current_zone = 0;
                counter = 0;
            } else if (current_zone < playerZone)
            {
                // Transition to zone 2
                PlaySong(song0B);
                current_zone = 1;
                counter = 0;
            } else
            {
                List<AudioClip> order;
                if (current_zone == 0)
                {
                    order = order0;
                } else
                {
                    order = order1;
                }

                PlaySong(order[counter]);

                ++counter;
                if (counter >= order.Count)
                {
                    counter = 0;
                }
            }
        }
    }

    void PlaySong(AudioClip song)
    {
        if (current_player == 1)
        {
            player2.PlayOneShot(song);
            current_player = 2;
        }
        else
        {
            player1.PlayOneShot(song);
            current_player = 1;
        }
    }
}
