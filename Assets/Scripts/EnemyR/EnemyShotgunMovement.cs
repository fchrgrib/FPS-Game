using UnityEngine;

public class EnemyShotgunMovement : EnemyMovement
{
    public float distanceThreshold = 15f;
    public float minDistanceFromPlayer = 5f;
    public float rotationDamping = 4f;
    private float distanceToPlayer;

    private void LateUpdate()
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
    }

    protected override void LookForPlayer()
    {
        if (distanceToPlayer < distanceThreshold && distanceToPlayer > minDistanceFromPlayer)
        {
            navMeshAgent.updateRotation = true;
            GoToPlayer();
        }
        else if (distanceToPlayer <= minDistanceFromPlayer)
        {
            MoveAway();
        }
        else
        {
            navMeshAgent.updateRotation = true;
            Idle();
        }
        
        animator.SetBool("IsWalking", navMeshAgent.velocity.magnitude != 0f);
    }
    
    private void MoveAway()
    {
        navMeshAgent.updateRotation = false;
        // transform.LookAt(playerTransform);
        var rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        
        var point = playerTransform.position + (transform.position - playerTransform.position).normalized * minDistanceFromPlayer;
        SetDestination(point);
    }
}