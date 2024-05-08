using System;
using UnityEngine;

public class AnimationEventListeners : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAttack enemyAttack;
    
    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    public void StartSinking()
    {
        enemyManager.Dead = true;
    }

    public void Attack()
    {
        enemyAttack.DealDamage();
    }
}