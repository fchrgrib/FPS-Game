using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralStatistics : MonoBehaviour
{
    public float elapsedTime = 0;
    public float travelDistance = 0;
    public float accuracy = 0;
    public int totalHitCount = 0;
    public int totalBulletCount = 0;
    public int totalKillCount = 0;
    public int totalDeathCount= 0;
    public float killdeath = 0;

    public static GeneralStatistics Instance;
    
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        ElapsedTime += Time.unscaledDeltaTime;
    }

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

    public int TotalHitCount
    {
        get => totalHitCount;
        set
        {
            totalHitCount = value;
        }
    }
    
    public int TotalBulletCount
    {
        get => totalBulletCount;
        set
        {
            totalBulletCount = value;
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
    public float KillDeathRatio()
    {
        return (float) totalKillCount / totalDeathCount;
    }
    
    public float GetAccuracy()
    {
        return (float) totalHitCount / totalBulletCount;
    }
    
}