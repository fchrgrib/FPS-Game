using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    public float minSpawnThreshold = 2f;
    public float maxSpawnThreshold = 5f;
    public int maxMobs = 10;
    
    public List<GameObject> mobs;
    
    private float spawnTimer;
    private float randomSpawnThreshold;
    private int currentMobs;

    void Awake()
    {
        randomSpawnThreshold = Random.Range(minSpawnThreshold, maxSpawnThreshold);
    }
    
    void FixedUpdate()
    {
        if (currentMobs >= maxMobs) return;
        
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
        var randomIndex = Random.Range(0, mobs.Count);
        NavMesh.SamplePosition(GeneratedPosition(), out var hit, Mathf.Infinity, NavMesh.AllAreas);
        Instantiate(mobs[randomIndex], hit.position, Quaternion.identity);
    }
}