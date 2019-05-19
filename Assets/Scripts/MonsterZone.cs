using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZone : MonoBehaviour
{
    public int pyramidNum = 0;
    public int sirenNum = 0;

    public EnemyPyramid pyramidPrefab;
    public EnemySiren sirenPrefab;

    private BoxCollider2D box;
    private Vector2 xRange;
    private Vector2 yRange; 

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();

        SpawnMonsters();
    }

    public void SpawnMonsters()
    {
        foreach (Transform child in transform)
        {
            Enemy enemy = child.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.DestroyIfInactive();
            }
        }

        for (int i = 0; i < pyramidNum; ++i)
        {
            EnemyPyramid newPyramid = Instantiate(pyramidPrefab, GeneratePosition(), Quaternion.identity);
            newPyramid.transform.parent = transform;
        }

        for (int i = 0; i < sirenNum; ++i)
        {
            EnemySiren newSiren = Instantiate(sirenPrefab, GeneratePosition(), Quaternion.identity);
            newSiren.transform.parent = transform;
        }
    }

    Vector2 GeneratePosition()
    {
        float x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        float y = Random.Range(box.bounds.min.y, box.bounds.max.y);

        return new Vector2(x, y);
    }

}
