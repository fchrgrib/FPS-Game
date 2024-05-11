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
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StartListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        missionText.SetText(SetTextMission());
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        EventManager.StopListening("AdmiralOfEnemyDeath", IncrementAdmiralOfEnemyDeathCount);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        if (EnemyLeaderDeathCount >= 4 && EnemyAdmiralDeathCount >= 1)
        {
            finalBox.SetActive(true);
            finalGate.SetActive(false);  
        }
    }

    public void LoadData(GameData data)
    {
        if (data.currentLevel > 3)
        {
            return;
        }
        Debug.Log("Loading Level 3");
        EnemyDeathCount = data.currentKillCount;
        EnemyLeaderDeathCount = data.currentLeaderKillCount;
        EnemyAdmiralDeathCount = data.currentAdmiralKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 3)
        {
            return;
        }

        data.currentLevel = 3;
        data.currentKillCount = EnemyDeathCount;
        data.currentLeaderKillCount = EnemyLeaderDeathCount;
        data.currentAdmiralKillCount = EnemyAdmiralDeathCount;
    }
}
