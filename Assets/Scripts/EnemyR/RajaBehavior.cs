public class RajaBehavior : JenderalBehavior
{
    public float speedDecreaseMultiplier = 0.6f;
    public float damageDecreaseMultiplier = 0.8f;
    private PlayerMovement playerMovement;
    
    private bool applied;
    
    protected override void Awake()
    {
        base.Awake();
        playerMovement = Player.GetComponent<PlayerMovement>();
    }
    
    protected override void ApplyEffectsToPlayer()
    {
        base.ApplyEffectsToPlayer();
        if (!applied) {
            playerMovement.ChangeSpeed(playerMovement.Speed * speedDecreaseMultiplier);
            PlayerManager.PlayerDamageMultiplier *= damageDecreaseMultiplier;
            applied = true;
        }
    }
    
    protected override void DispelEffects()
    {
        playerMovement.ChangeSpeed(playerMovement.Speed / speedDecreaseMultiplier);
        PlayerManager.PlayerDamageMultiplier /= damageDecreaseMultiplier;
        applied = false;
    }
}