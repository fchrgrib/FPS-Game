using System.Collections.Generic;
using Nightmare.Enum;
using UnityEngine;
using UnityEngine.AI;


public class MobSpawner : MonoBehaviour
{
    public float minSpawnThreshold = 2f;
    public float maxSpawnThreshold = 5f;
    public int maxMobs = 10;
    
    public GameObject mobs;
    public Difficulty Difficulty;
    
    private float spawnTimer;
    private float randomSpawnThreshold;
    private int currentMobs;

    void Awake()
    {
        randomSpawnThreshold = Random.Range(minSpawnThreshold, maxSpawnThreshold);

        switch (Difficulty)
        {
            case Difficulty.Medium:
                if (mobs.name == "Keroco")
                {
                    maxMobs += 20;
                }

                if (mobs.name == "Kepala Keroco")
                {
                    maxMobs += 6;
                }

                if (mobs.name == "JenderalWithPet")
                {
                    maxMobs += 2;
                }

                minSpawnThreshold = 1f;
                maxSpawnThreshold = 3f;
                break;
            
            case Difficulty.Hard:
                if (mobs.name == "Keroco")
                {
                    maxMobs += 40;
                }

                if (mobs.name == "Kepala Keroco")
                {
                    maxMobs += 12;
                }

                if (mobs.name == "JenderalWithPet")
                {
                    maxMobs += 4;
                }

                minSpawnThreshold = 0.5f;
                maxSpawnThreshold = 2f;
                break;
        }
    }
    
    void FixedUpdate()
    {
        if (currentMobs > maxMobs) return;
        
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer > randomSpawnThreshold)
        {
            SpawnMob();
            spawnTimer = 0f;
            randomSpawnThreshold = Random.Range(minSpawnThreshold, maxSpawnThreshold);
            currentMobs++;
        }
    }

    private Vector3 GeneratedPosition()
    {
        return new Vector3(Random.Range(0f, 100f), 100f, Random.Range(0f, 100f));
    }

    private void SpawnMob()
    {
        NavMesh.SamplePosition(GeneratedPosition(), out var hit, Mathf.Infinity, NavMesh.AllAreas);
        GameObject obj = Instantiate(mobs, hit.position, Quaternion.identity);
        obj.SetActive(true);
    }
}