using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: add game over controller
public class Level01Controller : MonoBehaviour
{
    public GameObject finalGate;

    public GameObject finalBox;

    public int enemyDeathCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", incrementEnemyDeathCount);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("EnemyDeath", incrementEnemyDeathCount);
    }

    void incrementEnemyDeathCount()
    {
        enemyDeathCount++;
    }
    
    void Update()
    {
        if (enemyDeathCount>=10)
        {
            finalGate.SetActive(false);
            finalBox.SetActive(true);
        }    
    }
}
