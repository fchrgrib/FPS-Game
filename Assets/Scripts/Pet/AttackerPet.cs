using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPet : DefaultPetMovement
{
    
    public float detectionRadius = 5f;
    public LayerMask enemyLayerMask;

    private Collider currentCollider;

    public override Vector3 DoActionAndGetDestination(PlayerManager playerManager, GameObject player)
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, detectionRadius,
            enemyLayerMask);
        
        if (hitColliders.Length == 0)
        {
            return base.DoActionAndGetDestination(playerManager, player);
        }
        
        // TODO: make sure the destination is sorted by nearest
        var destination = hitColliders[0].transform.position;
        var movementNormalized = destination - transform.position;
        movementNormalized.y = 0;   
        Quaternion targetRotation = Quaternion.LookRotation(movementNormalized, Vector3.up),
            smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        transform.rotation = smoothedRotation;
        
        return destination;
    }
    
}
