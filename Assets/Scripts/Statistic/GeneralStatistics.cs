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
            elapsedTime = value;
        }
    }

    public float TravelDistance
    {
        get => travelDistance;
        set
        {
            travelDistance = value;
        }
    }

    public float Accuracy
    {
        get => accuracy;
        set
        {
            accuracy = value;
        }
    }
    
    public void UpdateAccuracy(int hitCount, int bulletCount)
    {
        totalBulletCount += bulletCount;
        totalHitCount += hitCount;
        accuracy = (totalHitCount / totalBulletCount) * 100;


    }

    public int Death
    {
        get => totalDeathCount;
        set
        {
            totalDeathCount = value;
        }
    }
    public void DeathCount(int deathCount)
    {
        totalDeathCount += deathCount;
    }

    public int Kill
    {
        get => totalKillCount;
        set
        {
            totalKillCount = value;
        }
    }
    public void KillCount(int killCount)
    {
        totalKillCount += killCount;
    }

    public float KillDeath
    {
        get => killdeath;
        set 
        {
            killdeath = value;
        } 
    }
    public void KillDeathRatio(int killCount)
    {
        killdeath = totalKillCount / totalDeathCount;
    }
    
    
    
}