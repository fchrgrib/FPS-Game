public class HealthOrbBehavior : OrbBehaviour
{
    protected override void ExecuteOrbPerk(PlayerManager playerManager, PlayerMovement playerMovement)
    {
        if (playerManager.PlayerHp >= 85)
        {
            playerManager.PlayerHp = 100;
            return;
        }

        playerManager.PlayerHp += 20;
    }
}