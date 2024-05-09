using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerPet : DefaultPetMovement
{
    
    private float elapsedTime;
    private const float HealTime = 2f;

    public override Vector3 DoActionAndGetDestination(PlayerManager playerManager, GameObject player, 
        NavMeshAgent navMeshAgent, Animator animator)
    {
        if (elapsedTime - HealTime >= 0)
        {
            playerManager.PlayerHp += 1;
            elapsedTime = 0;
        }
        
        elapsedTime += Time.deltaTime;
        return base.DoActionAndGetDestination(playerManager, player, navMeshAgent, animator);
    }
    
}
