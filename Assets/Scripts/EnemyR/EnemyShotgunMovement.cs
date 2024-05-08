using System;
using UnityEngine;

public class EnemyShotgunMovement : EnemyMovement
{
    public float distanceThreshold = 15f;
    public float minDistanceFromPlayer = 10f;
    private float distanceToPlayer;

    private int angle;
    
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
            CircleAroundPlayer();
        }
        else
        {
            navMeshAgent.updateRotation = true;
            Idle();
        }
        
        animator.SetBool("IsWalking", navMeshAgent.velocity.magnitude != 0f);
    }
    
    private void CircleAroundPlayer()
    {
        navMeshAgent.updateRotation = false;
        transform.LookAt(playerTransform);

        // Circle around player
        angle = (angle + 5) % 360;
        var sideways = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
        
        var destination = playerTransform.position + sideways * 5f;
        SetDestination(destination);
    }
}