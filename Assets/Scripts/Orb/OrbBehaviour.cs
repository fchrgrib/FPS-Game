using UnityEngine;

public abstract class OrbBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    private readonly float countdownDuration = 5f;
    private float destroyTime;
    private readonly float detectionRadius = 5f;

    public void Start()
    {
        destroyTime = Time.time + countdownDuration;
    }

    private void Update()
    {
        // If 5 seconds have elapsed since the orb was instantiated...
        if (Time.time >= destroyTime)
        {
            // ...destroy the orb
            DestroyOrb();
        }

        // If the player is not colliding with the orb...
        var hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayerMask);
        if (hitColliders.Length != 1)
        {
            // ...return the method
            return;
        }

        // If the player is colliding:
        // 1. Execute the orb perk
        // 2. Remove the orb
        var playerManager = hitColliders[0].GetComponentInParent<PlayerManager>();
        var playerMovement = hitColliders[0].GetComponentInParent<PlayerMovement>();
        ExecuteOrbPerk(playerManager, playerMovement);
        DestroyOrb();
    }

    private void DestroyOrb()
    {
        Destroy(gameObject);
    }

    protected abstract void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement);
}