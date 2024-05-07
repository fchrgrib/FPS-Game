using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerPet : DefaultPetMovement
{
    
    private float elapsedTime;
    private const float HealTime = 2f;

    public override Vector3 DoActionAndGetDestination(PlayerManager playerManager, GameObject player)
    {
        if (elapsedTime - HealTime >= 0)
        {
            playerManager.PlayerHp += 1;
            elapsedTime = 0;
        }
        
        elapsedTime += Time.deltaTime;
        return base.DoActionAndGetDestination(playerManager, player);
    }
    
}
