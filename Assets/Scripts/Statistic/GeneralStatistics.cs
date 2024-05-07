using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStatistics : MonoBehaviour
{
    private float elapsedTime = 0;
    private float travelDistance = 0;
    private float accuracy = 0;
    private int totalHitCount = 0;
    private int totalBulletCount = 0;
    private int totalKillCount = 0;
    private int totalDeathCount= 0;
    private float killdeath = 0;
    

    public float ElapsedTime
    {
        get => elapsedTime;
        set
        {
            
        }
    }
    
    public float TravelDistance { get; set; }
    
    public float Accuracy { get; set; }
    
    public void UpdateAccuracy(int hitCount, int bulletCount)
    {
        totalBulletCount += bulletCount;
        totalHitCount += hitCount;
        accuracy = (totalHitCount / totalBulletCount) * 100;


    }
    
    public float Death { get; set; }
    public void DeathCount(int deathCount)
    {
        totalDeathCount += deathCount;
    }
    
    public float Kill { get; set; }
    public void KillCount(int killCount)
    {
        totalKillCount += killCount;
    }
    
    public float KillDeath { get; set; }
    public void KillDeathRatio(int killCount)
    {
        killdeath = totalKillCount / totalDeathCount;
    }
    
    
    
}