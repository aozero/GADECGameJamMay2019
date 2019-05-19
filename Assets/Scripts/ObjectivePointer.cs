using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    private SpriteRenderer arrowSprite;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Globals.player;

        arrowSprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.objectiveSet)
        {
            arrowSprite.enabled = true;

            // Move position to center of player
            transform.position = player.transform.position;
            // Point towards the objective
            transform.up = player.GetObjective() - transform.position;
        } else
        {
            // Don't show arrow if there is no objective
            arrowSprite.enabled = false;
        }
    }
}
