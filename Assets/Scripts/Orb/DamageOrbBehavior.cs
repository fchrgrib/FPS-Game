public class DamageOrbBehavior : OrbBehaviour
{
    protected override void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement)
    {
        // A player can only get a maximum amount of 15 damage orbs
        if (playerManager.DamageOrbCount == 15)
        {
            return;
        }

        playerManager.PlayerDamageMultiplier += 0.1f;
        playerManager.DamageOrbCount += 1;
    }
}