using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: add game over controller
public class Level03Controller : MonoBehaviour, IDataPersistence
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int EnemyAdmiralDeathCount { get; private set; }
    public TMP_Text missionText;

    public int maxLeaderOfKerocoDeath = 4;
    public int maxAdmiralOfKerocoDeath = 1;

    public GameObject finalBox;
    public GameObject finalGate;

    private bool finished = false;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StartListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        missionText.SetText(SetTextMission());
    }
    
    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StopListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
    }
    
    private string SetTextMission()
    {
        return $"Your Mission\nKill Kepala Keroco   {EnemyLeaderDeathCount}/{maxLeaderOfKerocoDeath}\nKill Jenderal     {EnemyAdmiralDeathCount}/{maxAdmiralOfKerocoDeath}";
    }
    
    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
    }

    private void IncrementLeaderOfEnemyDeathCount()
    {
        EnemyLeaderDeathCount++;
        missionText.SetText(SetTextMission());
    }
    
    private void IncrementAdmiralOfEnemyDeathCount()
    {
        EnemyAdmiralDeathCount++;
        missionText.SetText(SetTextMission());
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && EnemyLeaderDeathCount >= 4 && EnemyAdmiralDeathCount >= 1)
        {
            finalBox.SetActive(true);
            finalGate.SetActive(false);  
            
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
        if (data.currentLevel > 3)
        {
            return;
        }
        
        Debug.Log("Loading Level 3");
        EnemyDeathCount = data.Level3.currentKillCount;
        EnemyLeaderDeathCount = data.Level3.currentLeaderKillCount;
        EnemyAdmiralDeathCount = data.Level3.currentAdmiralKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 3)
        {
            return;
        }

        data.currentLevel = 3;
        data.Level3.currentKillCount = EnemyDeathCount;
        data.Level3.currentLeaderKillCount = EnemyLeaderDeathCount;
        data.Level3.currentAdmiralKillCount = EnemyAdmiralDeathCount;
    }
}
