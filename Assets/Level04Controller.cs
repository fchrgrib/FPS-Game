using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level04Controller : MonoBehaviour
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int EnemyAdmiralDeathCount { get; private set; }
    public int EnemyKingDeathCount { get; private set; }
    public TMP_Text missionText;

    public int maxLeaderKerocoDeath = 2;
    public int maxAdmiralOfKerocoDeath = 2;
    public int maxKingOfKerocoDeath = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StartListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        EventManager.StartListening("KingOfEnemyDeath", IncrementKingOfEnemyDeathCount);
        missionText.SetText(SetTextMission());
    }
    
    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StopListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        EventManager.StopListening("KingOfEnemyDeath", IncrementKingOfEnemyDeathCount);
    }

    private string SetTextMission()
    {
        return $"Your Mission\nKill Kepala Keroco   {EnemyLeaderDeathCount}/{maxLeaderKerocoDeath}\nKill Jenderal     {EnemyAdmiralDeathCount}/{maxAdmiralOfKerocoDeath}\nKill King     {EnemyKingDeathCount}/{maxKingOfKerocoDeath}";
    }
    
    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
        missionText.SetText(SetTextMission());
    }

    private void IncrementLeaderOfEnemyDeathCount()
    {
        EnemyLeaderDeathCount++;
    }
    
    private void IncrementAdmiralOfEnemyDeathCount()
    {
        EnemyAdmiralDeathCount++;
        missionText.SetText(SetTextMission());
    }

    private void IncrementKingOfEnemyDeathCount()
    {
        EnemyKingDeathCount++;
        missionText.SetText(SetTextMission());
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyKingDeathCount>=maxKingOfKerocoDeath && EnemyAdmiralDeathCount>=maxAdmiralOfKerocoDeath && EnemyLeaderDeathCount >= maxLeaderKerocoDeath)
        {
            //TODO: Do something if king of enemy die  
        }
    }
}
