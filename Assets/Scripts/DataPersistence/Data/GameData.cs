using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int deathCount;
    public Vector3 playerPosition;

    public GameData()
    {
        this.deathCount = 100000;
        this.playerPosition = Vector3.zero;
        this.playerPosition.y = 1;
    }
    public int GetTime()
    {
        //return save time
        
        // float x = playerPosition.x;
        // float y = playerPosition.y;
        // float z = playerPosition.z;
        //return x+ y + z;
        
        return 12333;
    }
    public string GetFileName()
    {
        //return file name
        return "tesssss";
    }
}