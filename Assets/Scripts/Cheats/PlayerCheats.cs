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

    private static bool _doubleSpeedApplied;

    #region Toggleable Cheats

    public static void HandleTakingDamage(this CheatListener listener, Action normalAction)
        {
            if (noDamage)
            {
                return;
            }
            
            normalAction();
        }
    
        public static bool HandleDealingDamage(this CheatListener listener, Action normalAction)
        {
            // set playerDamageMultiplier to max value?
            throw new NotImplementedException();
        }
        
        public static void HandleSpeed(this CheatListener listener, ref float speed)
        {
            switch (doubleSpeed)
            {
                case true when !_doubleSpeedApplied:
                    speed *= 2;
                    _doubleSpeedApplied = true;
                    break;
                case false when _doubleSpeedApplied:
                    speed /= 2;
                    _doubleSpeedApplied = false;
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