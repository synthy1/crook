using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    float timeToNextSpawn;
    float timeSinceLastSpawned = 0.0f;

    
    public float SpawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextSpawn = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (timeSinceLastSpawned > timeToNextSpawn)
        {
            int selction = Random.Range(0, objectsToSpawn.Length);
            Instantiate(objectsToSpawn[selction], transform.position, Quaternion.identity);
            timeToNextSpawn = SpawnTime;
            timeSinceLastSpawned = 0.0f;
        }
    }
}
