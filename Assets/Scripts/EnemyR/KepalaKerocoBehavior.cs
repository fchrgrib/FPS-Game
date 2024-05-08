using UnityEngine;
using UnityEngine.Serialization;

public class KepalaKerocoBehavior : MonoBehaviour
{
    public int kerocoSpawnRate = 25;
    public GameObject prefab;

    void Awake()
    {
        InvokeRepeating(nameof(SpawnKeroco), 0, kerocoSpawnRate);
    }
    
    private void SpawnKeroco()
    {
        var spawnPosition = transform.position + Random.insideUnitSphere * 5;
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}