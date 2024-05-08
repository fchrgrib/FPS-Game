using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public float visionRadius = 10f;
    public int minIdleTime = 2;
    public int maxIdleTime = 5;
    
    private UnityAction<bool> pauseListener;
    private float currentVision;
    protected Transform playerTransform;
    protected PlayerManager playerManager;
    protected EnemyManager enemyManager;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    
    private float timer;
    private bool isPaused;
    private const float WanderDistance = 15f;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerOnly = GameObject.Find("PlayerOnly");
        playerTransform = playerOnly.transform;
        playerManager = player.GetComponent<PlayerManager>();
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        pauseListener = Pause;
        EventManager.StartListening("Pause", Pause);
    }

    void OnEnable()
    {
        navMeshAgent.enabled = true;
        ResetPath();
        timer = 0f;
    }

    void OnDestroy()
    {
        EventManager.StopListening("Pause", Pause);
    }

    void FixedUpdate()
    {
        if (isPaused) return;

        if (enemyManager.IsDead())
        {
            ResetPath();
        }
        
        if (enemyManager.health > 0 && playerManager.PlayerHp > 0)
        {
            LookForPlayer();
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    private void ResetPath()
    {
        if (navMeshAgent.hasPath) navMeshAgent.ResetPath();
    }

    private void Pause(bool state)
    {
        isPaused = state;
        if (navMeshAgent.hasPath)
            navMeshAgent.isStopped = isPaused;
    }

    protected virtual void LookForPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= visionRadius)
        {
            GoToPlayer();
        }
        else
        {
            Idle();
        }
        
        animator.SetBool("IsWalking", navMeshAgent.velocity.magnitude != 0f);
    }
    
    public void GoToPlayer()
    {
        SetDestination(playerTransform.position);
    }
    
    protected void SetDestination(Vector3 destination)
    {
        timer = -1f;
        GoToDestination(destination);
    }

    private void GoToDestination(Vector3 destination)
    {
        if (!enemyManager.IsDead() && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(destination);
        }
    }

    protected void Idle()
    {
        if (timer <= 0f)
        {
            // Wander around
            var randomPosition = Random.insideUnitSphere * WanderDistance + transform.position;
            NavMesh.SamplePosition(randomPosition, out var hit, WanderDistance, NavMesh.AllAreas);
            GoToDestination(hit.position);
            
            timer = Random.Range(2, 5);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}