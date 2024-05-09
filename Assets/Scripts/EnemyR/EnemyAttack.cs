using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;
    public float attackDamageMultiplier = 1f;
        
    private UnityAction<bool> pauseListener;
    private Animator anim;
    private GameObject player;
    protected PlayerManager PlayerManager;
    protected EnemyManager EnemyManager;

    private bool playerInRange;
    protected bool IsPaused;
    private float timer;
    private bool isAttacking;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager = player.GetComponent<PlayerManager>();
        EnemyManager = GetComponent<EnemyManager>();
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
            playerInRange = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    protected virtual void Update()
    {
        if (IsPaused) return;
        
        timer += Time.deltaTime;

        Attack();
    }

    private void Pause(bool state)
    {
        IsPaused = state;
    }

    protected virtual void Attack()
    {
        if (!(timer >= timeBetweenAttacks) || !playerInRange || !(EnemyManager.health > 0) || isAttacking) return;
        timer = 0f;
    
        if (PlayerManager.PlayerHp > 0)
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    public void DealDamage()
    {
        PlayerManager.TakeDamage(attackDamage * attackDamageMultiplier);
        isAttacking = false;
    }
}