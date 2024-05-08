using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> orbPrefabs;
    private float nextSpawnTime;
    private readonly float spawnInterval = 15f;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time < nextSpawnTime) return;

        SpawnOrb();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void SpawnOrb()
    {
        var orbToSpawn = orbPrefabs[Random.Range(0, orbPrefabs.Count)];
        var spawnLocation = transform.position;
        Instantiate(orbToSpawn, spawnLocation, Quaternion.identity);
    }
}