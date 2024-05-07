using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOrbBehavior : OrbsBehaviourScript
{
    protected override void PlayerEnter(PlayerManager playerManager)
    {
        playerManager.PlayerDamageMultiplier += 0.1f;
    }
}
