using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level04Controller : MonoBehaviour, IDataPersistence
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int EnemyAdmiralDeathCount { get; private set; }
    public int EnemyKingDeathCount { get; private set; }
    public TMP_Text missionText;

    public int maxLeaderKerocoDeath = 2;
    public int maxAdmiralOfKerocoDeath = 2;
    public int maxKingOfKerocoDeath = 1;

    public bool finished = false;
    
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
        if (!finished && EnemyKingDeathCount>=maxKingOfKerocoDeath && EnemyAdmiralDeathCount>=maxAdmiralOfKerocoDeath 
            && EnemyLeaderDeathCount >= maxLeaderKerocoDeath)
        {
            SceneManager.LoadScene("EndingCutScene");  

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
            finished = true;
        }
    }

    public void LoadData(GameData data)
    {
        if (data.currentLevel != 4)
        {
            return;
        }

        Debug.Log("Loading Level 4");
        EnemyDeathCount = data.Level4.currentKillCount;
        EnemyLeaderDeathCount = data.Level4.currentLeaderKillCount;
        EnemyAdmiralDeathCount = data.Level4.currentAdmiralKillCount;
        EnemyKingDeathCount = data.Level4.currentKingKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 4)
        {
            return;
        }

        data.currentLevel = 4;
        data.Level4.currentKillCount = EnemyDeathCount;
        data.Level4.currentLeaderKillCount = EnemyLeaderDeathCount;
        data.Level4.currentAdmiralKillCount = EnemyAdmiralDeathCount;
        data.Level4.currentKingKillCount = EnemyKingDeathCount;
    }
}