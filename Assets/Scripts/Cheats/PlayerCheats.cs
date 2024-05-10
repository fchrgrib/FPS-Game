using System;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public static void Motherlode()
    {
        PlayerManager.PlayerMoney = int.MaxValue;
        EventManager.TriggerEvent("MotherlodeCheat");
    }

    public static void PetImmune()
    {
        throw new NotImplementedException();
    }

    public static void KillPet()
    {
        // Kill own pet or enemy pet?
        EventManager.TriggerEvent("KillPetCheat");
    }

    public static void GetOrb(GameObject orbPrefab)
    {
        var playerTransform = GameObject.Find("PlayerOnly").transform;
        Object.Instantiate(orbPrefab, playerTransform.position, Quaternion.identity);
    }

    public static void Skip()
    {
        LevelManager.currentLevelManager.NextLevel();
    }

    #endregion
}