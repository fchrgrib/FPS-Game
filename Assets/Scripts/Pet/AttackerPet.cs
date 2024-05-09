using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackerPet : DefaultPetMovement
{
    
    public float detectionRadius = 5f;
    public LayerMask enemyLayerMask;

    private Collider currentCollider;

    public override Vector3 DoActionAndGetDestination(PlayerManager playerManager, GameObject player, 
        NavMeshAgent navMeshAgent, Animator animator)
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, detectionRadius,
            enemyLayerMask);
        
        if (hitColliders.Length == 0)
        {
            animator.SetBool("Attacking", false);
            return base.DoActionAndGetDestination(playerManager, player, navMeshAgent, animator);
        }

        Collider target = hitColliders[0];
        float minDistance = float.MaxValue;
        foreach (Collider enemy in hitColliders)
        {
            Vector3 vectorToTarget = enemy.transform.position - transform.position;
            float distanceToTarget = vectorToTarget.sqrMagnitude;
            if (distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                target = enemy;
            }
        }

        bool attacking = minDistance <= 1.1 * navMeshAgent.stoppingDistance;
        animator.SetBool("Attacking", attacking);
        if (attacking)
        {
            target.GetComponent<EnemyManager>()?.TakeDamage(20, target.transform.position);
        }
        
        var destination = target.transform.position;
        var movementNormalized = destination - transform.position;
        movementNormalized.y = 0;   
        Quaternion targetRotation = Quaternion.LookRotation(movementNormalized, Vector3.up),
            smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        transform.rotation = smoothedRotation;
        
        return destination;
    }
    
}
