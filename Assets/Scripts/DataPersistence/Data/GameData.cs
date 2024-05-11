using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string playerName = "default";
    public string lastUpdate;
    public long lastUpdated;
    public int deathCount;
    public Vector3 playerPosition;
    public int playerMoney = 300;
    public float playerHealth = 100;
    public string playerPet = "NO_PET";
    public int currentLevel;
    public int currentKillCount;
    public int currentLeaderKillCount;
    public int currentAdmiralKillCount;
    public int currentKingKillCount;

    public GameData()
    {
        this.deathCount = 0;
        this.playerPosition = Vector3.zero;
        this.playerPosition.y = 1;
    }
    public string GetTime()
    {
        return lastUpdate;
    }
    public string GetFileName()
    {
        return playerName;
    }
}