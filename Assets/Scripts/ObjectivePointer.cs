using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = Globals.player;
    }

    // Update is called once per frame
    void Update()
    {
        // Move position to center of player
        transform.position = player.transform.position;
        // Point towards the objective
        transform.up = player.objectiveLocation - transform.position;
    }
}
