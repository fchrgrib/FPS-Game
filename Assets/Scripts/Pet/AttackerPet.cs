using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPet : DefaultPetMovement
{
    
    public float detectionRadius = 5f;
    public float attackingDistance = 1.5f;
    public LayerMask enemyLayerMask;

    private Collider currentCollider;
    
    public override void MovePet(Vector3 movement, Vector3 playerPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, detectionRadius, enemyLayerMask);

        if (hitColliders.Length == 0)
        {
            base.MovePet(movement, playerPosition);
            return;
        }
        
        // TODO: check if enemy has hied, if so change the enemy
        if (currentCollider is null)
        {
            currentCollider = hitColliders[0];
        }
        
        base.MovePet((currentCollider.transform.position - transform.position) * Time.deltaTime,
            currentCollider.transform.position);

        if ((currentCollider.transform.position - transform.position).magnitude < attackingDistance)
        {
            animator.SetBool("Walking", false);
        }
    }
}
