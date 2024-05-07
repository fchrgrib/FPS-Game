using UnityEngine;

public abstract class OrbBehaviour : MonoBehaviour
{
    public float countdownDuration = 5f;
    public float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayerMask;

    private bool used;

    public void Start()
    {
        Invoke(nameof(RemoveOrb), countdownDuration);
    }

    public void RemoveOrb()
    {
        if (used)
        {
            return;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        // If the player is not colliding with the orb...
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayerMask);
        if (hitColliders.Length != 1)
        {
            // Return the method
            return;
        }

        // If the player is colliding:
        // 1. Execute the orb perk
        // 2. Mark it as used
        // 3. Remove it
        PlayerManager playerManager = hitColliders[0].GetComponentInParent<PlayerManager>();
        PlayerMovement playerMovement = hitColliders[0].GetComponentInParent<PlayerMovement>();
        ExecuteOrbPerk(playerManager, playerMovement);
        used = true;
        RemoveOrb();
    }

    protected abstract void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement);
}