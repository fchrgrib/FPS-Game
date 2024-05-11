[System.Serializable]
public class Level1
{
    public int currentKillCount;
    
    public Level1()
    {
        currentKillCount = 0;
    }
    
}
    
[System.Serializable]
public class Level2
{
    public int currentKillCount;
    public int currentLeaderKillCount;
    
    public Level2()
    {
        currentKillCount = 0;
        currentLeaderKillCount = 0;
    }
}
    
[System.Serializable]
public class Level3
{
    public int currentKillCount;
    public int currentLeaderKillCount;
    public int currentAdmiralKillCount;
    
    public Level3()
    {
        currentKillCount = 0;
        currentLeaderKillCount = 0;
        currentAdmiralKillCount = 0;
    }
}
    
[System.Serializable]
public class Level4
{
    public int currentKillCount;
    public int currentLeaderKillCount;
    public int currentAdmiralKillCount;
    public int currentKingKillCount;
    
    public Level4()
    {
        currentKillCount = 0;
        currentLeaderKillCount = 0;
        currentAdmiralKillCount = 0;
        currentKingKillCount = 0;
    }
}