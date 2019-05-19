using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckSpawner : MonoBehaviour
{
    // Min and max distances that wrecks can spawn
    // Used for both X/Y and  -/+
    public float minDistance; 
    public float maxDistance;

    public float maxDistanceIncrease;
    public float minDistanceIncrease;

    public WreckSite wreckPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Globals.wreckSpawner = this;

        SpawnWreck();
    }

    public void SpawnWreck()
    {
        // 0 is negative, 1 is positive
        int xDirection = Random.Range(0, 2);
        if (xDirection == 0) { xDirection = -1; }

        int yDirection = Random.Range(0, 2);
        if (yDirection == 0) { yDirection = -1; }

        float xDistance = Random.Range(minDistance, maxDistance);
        float yDistance = Random.Range(minDistance, maxDistance);

        Vector3 position = new Vector3(xDirection * xDistance, yDirection * yDistance, 0);

        Instantiate(wreckPrefab, position, Quaternion.identity);

        maxDistance += maxDistanceIncrease;
        minDistance += minDistanceIncrease;
    }
}
