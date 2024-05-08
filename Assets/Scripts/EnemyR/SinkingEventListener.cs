using System;
using UnityEngine;

public class SinkingEventListener : MonoBehaviour
{
    private EnemyManager enemyManager;
    
    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    public void StartSinking()
    {
        enemyManager.Dead = true;
    }
}