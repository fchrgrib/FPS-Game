using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;
        
    protected UnityAction<bool> pauseListener;
    protected Animator anim;
    private GameObject player;
    protected PlayerManager playerManager;
    protected EnemyManager enemyManager;
    
    protected bool PlayerInRange;
    private bool isPaused;
    protected float Timer;
    protected bool IsAttacking;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        enemyManager = GetComponent<EnemyManager>();
        anim = GetComponentInChildren<Animator>();
        
        pauseListener = Pause;
        EventManager.StartListening("Pause", Pause);
    }

    void OnDestroy()
    {
        EventManager.StopListening("Pause", Pause);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerInRange = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPaused) return;
        
        Timer += Time.deltaTime;

        Attack();
    }

    private void Pause(bool state)
    {
        isPaused = state;
    }

    protected virtual void Attack()
    {
        if (!(Timer >= timeBetweenAttacks) || !PlayerInRange || !(enemyManager.health > 0) || IsAttacking) return;
        Timer = 0f;
    
        if (playerManager.PlayerHp > 0)
        {
            anim.SetTrigger("Attack");
            IsAttacking = true;
        }
    }

    public void DealDamage()
    {
        playerManager.TakeDamage(attackDamage);
        IsAttacking = false;
    }
}