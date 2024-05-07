using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Cheats : MonoBehaviour
{
    public bool noDamage;
    public bool oneHitKill;
    public bool doubleSpeed;
    
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