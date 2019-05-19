using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZoneManager : MonoBehaviour
{
    // There is a grid system, where home port is (0,0) and as you go out either -1 or 1 more monsters spawn
    public Vector2 numZonesX;
    public Vector2 numZonesY;

    public float zoneSizeX = 45;
    public float zoneSizeY = 45;

    // [0] is 0 zones away, [1] is 1 zone away, etc.
    public List<int> numPyramids;
    public List<int> numSirens;

    public MonsterZone zonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Globals.monsterZoneManager = this;

        for (int x = (int) numZonesX[0]; x <= numZonesX[1]; ++x)
        {
            for (int y = (int) numZonesY[0]; y <= numZonesY[1]; ++y)
            {
                Vector3 position = new Vector3(zoneSizeX * x, zoneSizeY * y, 0);
                MonsterZone newZone = Instantiate(zonePrefab, position, Quaternion.identity);
                newZone.transform.parent = transform;

                int zoneDifficulty = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                newZone.pyramidNum = numPyramids[zoneDifficulty];
                newZone.sirenNum = numSirens[zoneDifficulty];
            }
        }
    }

    public void RespawnMonsters()
    {
        foreach(Transform child in transform)
        {
            MonsterZone monsterZone = child.gameObject.GetComponent<MonsterZone>();

            if (monsterZone != null)
            {
                monsterZone.SpawnMonsters();
            }
        }
    }
}
