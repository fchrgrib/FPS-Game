using System;

public interface CheatListener
{
}

[Serializable]
public static class PlayerCheats
{
    public static bool noDamage;
    public static bool oneHitKill;
    public static bool doubleSpeed;

    public static bool doubleSpeedApplied;
    
    private static float _originalDamageMultiplier;

    #region Toggleable Cheats

    public static void HandleTakingDamage(this CheatListener listener, Action normalAction)
    {
        if (noDamage)
        {
            return;
        }

        normalAction();
    }

    public static void HandleDealingDamage(this CheatListener listener, PlayerManager playerManager)
    {
        _originalDamageMultiplier = playerManager.PlayerDamageMultiplier;
        
        if (oneHitKill)
        {
            playerManager.PlayerDamageMultiplier = 1000f;
        }
        else
        {
            playerManager.PlayerDamageMultiplier = _originalDamageMultiplier;
        }
    }

    public static void HandleSpeed(this CheatListener listener, PlayerMovement playerMovement, float speed)
    {
        switch (doubleSpeed)
        {
            case true when !doubleSpeedApplied:
                playerMovement.Speed *= 2;
                doubleSpeedApplied = true;
                break;
            case false when doubleSpeedApplied:
                playerMovement.Speed /= 2;
                doubleSpeedApplied = false;
                break;
            case false when !doubleSpeedApplied:
                playerMovement.Speed = speed;
                break;
        }
    }

    #endregion

    #region One Time Cheats

    public static void Motherlode(this CheatListener listener)
    {
        throw new NotImplementedException();
    }

    public static void PetImmune(this CheatListener listener)
    {
        throw new NotImplementedException();
    }

    public static void KillPet(this CheatListener listener)
    {
        throw new NotImplementedException();
    }

    public static void GetOrb(this CheatListener listener)
    {
        throw new NotImplementedException();
    }

    public static void Skip(this CheatListener listener)
    {
        throw new NotImplementedException();
    }

    #endregion
}