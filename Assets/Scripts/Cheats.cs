using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Cheats : MonoBehaviour
{
    public bool noDamage;
    public bool oneHitKill;
    public bool doubleSpeed;
    
    public GameObject orbPrefab;

    private static readonly Dictionary<KeyCode, UnityAction> CheatMap = new()
    {
        {KeyCode.M, PlayerCheats.Motherlode},
        {KeyCode.P, PlayerCheats.PetImmune},
        {KeyCode.K, PlayerCheats.KillPet},
        // {KeyCode.G, PlayerCheats.GetOrb},
        {KeyCode.N, PlayerCheats.Skip}
    };

    void Update()
    {
        if (!Input.GetKey(KeyCode.RightShift)) return;
        print("Input Detected For Cheats");
        
        // Special case for orb
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerCheats.GetOrb(orbPrefab);
            return;
        }
        
        foreach (var cheat in CheatMap.Where(cheat => Input.GetKeyDown(cheat.Key)))
        {
            cheat.Value.Invoke();
        }
    }
    
    void OnEnable()
    {
        print("Cheats Enabled");
        PlayerCheats.noDamage = noDamage;
        PlayerCheats.oneHitKill = oneHitKill;
        PlayerCheats.doubleSpeed = doubleSpeed;
    }
    
    void OnDisable()
    {
        print("Cheats Disabled");
        PlayerCheats.noDamage = false;
        PlayerCheats.oneHitKill = false;
        PlayerCheats.doubleSpeed = false;
    }
}