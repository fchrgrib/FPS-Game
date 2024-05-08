using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public float visionRadius = 10f;
    public Vector2 idleTimeRange;
    
    private UnityAction<bool> pauseListener;
    private float currentVision;
    private Transform playerTransform;
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private NavMeshAgent navMeshAgent;

    private float timer;
    private bool isPaused;
    private const float WanderDistance = 10f;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = playerTransform.GetComponent<PlayerManager>();
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
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

    void Update()
    {
        if (isPaused) return;
        
        if (enemyManager.health > 0 && playerManager.PlayerHp > 0)
        {
            LookForPlayer();
            Idle();
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
    }

    private void LookForPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= visionRadius)
        {
            SetDestination(playerTransform.position);
        }
    }
    
    public void GoToPlayer()
    {
        SetDestination(playerTransform.position);
    }
    
    private void SetDestination(Vector3 destination)
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

    private void Idle()
    {
        if (navMeshAgent.hasPath) return;

        if (timer <= 0f)
        {
            // Wander around
            var randomPosition = Random.insideUnitSphere * WanderDistance + transform.position;
            NavMesh.SamplePosition(randomPosition, out var hit, WanderDistance, NavMesh.AllAreas);
            GoToDestination(hit.position);

            if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                ResetPath();
            }
            
            timer = Random.Range(idleTimeRange.x, idleTimeRange.y);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}