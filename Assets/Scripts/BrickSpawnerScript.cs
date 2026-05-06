using UnityEngine;
using System.Collections.Generic;

public class BrickSpawnerScript : MonoBehaviour
{
    public GameObject brickPrefab;
    public int numberOfSlots = 8;
    public GameObject brickParent;
    private List<int> slots = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBrick(int numberOfBricksToSpawn)
    {
        randomSpawnPos(numberOfBricksToSpawn);

        for (int i=0; i<numberOfBricksToSpawn; i++)
        {
            Instantiate(brickPrefab, new Vector3(transform.position.x + slots[i], transform.position.y, 0), transform.rotation, brickParent.transform);
        }
    }

    private void randomSpawnPos(int n) // n = number of bricks to spawn
    {
        for (int i=0; i<numberOfSlots; i++)
        {
            slots.Add(i);
        }

        for (int i=0; i<n; i++)
        {
            int idx = Random.Range(i, slots.Count);
            (slots[i], slots[idx]) = (slots[idx], slots[i]);
        }
    }
}
