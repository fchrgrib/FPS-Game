using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level04Controller : MonoBehaviour
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int EnemyAdmiralDeathCount { get; private set; }
    public int EnemyKingDeathCount { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StartListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        EventManager.StartListening("KingOfEnemyDeath", IncrementKingOfEnemyDeathCount);
    }
    
    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StopListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        EventManager.StopListening("KingOfEnemyDeath", IncrementKingOfEnemyDeathCount);
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

    private void IncrementKingOfEnemyDeathCount()
    {
        EnemyKingDeathCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyKingDeathCount>0)
        {
            //TODO: Do something if king of enemy death  
        }
    }
}
