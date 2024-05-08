using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;
        
    private UnityAction<bool> pauseListener;
    private Animator anim;
    private GameObject player;
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    
    private bool playerInRange;
    private bool isPaused;
    private float timer;
    private bool isAttacking;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        enemyManager = GetComponent<EnemyManager>();
        anim = GetComponent<Animator>();
        
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

    void Update()
    {
        if (isPaused) return;
        
        timer += Time.deltaTime;

        Attack();
    }

    private void Pause(bool state)
    {
        isPaused = state;
    }

    private void Attack()
    {
        if (!(timer >= timeBetweenAttacks) || !playerInRange || !(enemyManager.health > 0) || isAttacking) return;
        timer = 0f;
    
        if (playerManager.PlayerHp > 0)
        {
            playerManager.TakeDamage(attackDamage);
        }
    }
}