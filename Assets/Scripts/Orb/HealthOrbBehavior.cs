using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbBehavior : OrbsBehaviourScript
{
    
    protected override void PlayerEnter(PlayerManager playerManager)
    {
        if (playerManager.PlayerHp >= 85)
        {
            playerManager.PlayerHp = 100;
            return;
        }
        playerManager.PlayerHp += 20;
    }
}
