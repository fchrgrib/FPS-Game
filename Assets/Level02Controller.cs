using TMPro;
using UnityEngine;

//TODO: add game over controller
public class Level02Controller : MonoBehaviour, IDataPersistence
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }
    public int maxKerocoDeath = 5;
    public int maxLeaderOfKerocoDeath = 4;

    public GameObject finalBox;
    public GameObject finalGate;
    public TMP_Text missionText;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        missionText.SetText(SetTextMission());
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private string SetTextMission()
    {
        return $"Your Mission\nKill Keroco     {EnemyDeathCount}/{maxKerocoDeath}\nKill Kepala Keroco   {EnemyLeaderDeathCount}/{maxLeaderOfKerocoDeath}";
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
        missionText.SetText(SetTextMission());
    }

    private void IncrementLeaderOfEnemyDeathCount()
    {
        EnemyLeaderDeathCount++;
        missionText.SetText(SetTextMission());
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLeaderDeathCount >= 4 && EnemyDeathCount>=5)
        {
            finalBox.SetActive(true);
            finalGate.SetActive(false);   
        }
    }

    public void LoadData(GameData data)
    {
        if (data.currentLevel > 2)
        {
            return;
        }
        Debug.Log("Loading Level 2");
        EnemyDeathCount = data.currentKillCount;
        EnemyLeaderDeathCount = data.currentLeaderKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 2)
        {
            return;
        }

        data.currentLevel = 2;
        data.currentKillCount = EnemyDeathCount;
        data.currentLeaderKillCount = EnemyLeaderDeathCount;
    }
}
