using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03Controller : MonoBehaviour
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int EnemyAdmiralDeathCount { get; private set; }

    public GameObject finalBox;
    public GameObject finalGate;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StartListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
    }
    
    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StopListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
    }
    
    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
    }

    private void IncrementLeaderOfEnemyDeathCount()
    {
        EnemyLeaderDeathCount++;
    }
    
    private void IncrementAdmiralOfEnemyDeathCount()
    {
        EnemyAdmiralDeathCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLeaderDeathCount >= 3 && EnemyAdmiralDeathCount >= 1)
        {
            finalBox.SetActive(true);
            finalGate.SetActive(false);  
        }
    }
}
