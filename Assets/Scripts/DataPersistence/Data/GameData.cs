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
    public int currentLevel = 1;

    public Level1 Level1 = new Level1();
    public Level2 Level2 = new Level2();
    public Level3 Level3 = new Level3();
    public Level4 Level4 = new Level4();
    
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