using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: add game over controller
public class Level02Controller : MonoBehaviour
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }

    public GameObject finalBox;
    public GameObject finalGate;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("EnemyLeaderDeath", IncrementEnemyLeaderDeathCount);
    }

    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("EnemyLeaderDeath", IncrementEnemyLeaderDeathCount);
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
    }

    private void IncrementEnemyLeaderDeathCount()
    {
        EnemyLeaderDeathCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLeaderDeathCount >= 3)
        {
            finalBox.SetActive(true);
            finalGate.SetActive(false);   
        }
    }
}
