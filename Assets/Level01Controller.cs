using TMPro;
using UnityEngine;


//TODO: add game over controller
public class Level01Controller : MonoBehaviour, IDataPersistence
{
    public GameObject finalGate;
    public int maxKerocoDeath = 10;

    public GameObject finalBox;
    public TMP_Text missionText;

    private bool finished = false;
    
    public int EnemyDeathCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        missionText.SetText(SetTextMission());
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private string SetTextMission()
    {
        return $"Your Mission\nKill Keroco {EnemyDeathCount}/{maxKerocoDeath}";
    }

    private void OnDestroy()
    {
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
        missionText.SetText(SetTextMission());
    }
    
    void Update()
    {
        if (!finished && EnemyDeathCount>=10)
        {
            finalGate.SetActive(false);
            finalBox.SetActive(true);
            
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
        if (data.currentLevel > 1)
        {
            return;
        }
        Debug.Log("Loading Level 1");

        EnemyDeathCount = data.Level1.currentKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 1)
        {
            return;
        }

        data.currentLevel = 1;
        data.Level1.currentKillCount = EnemyDeathCount;
    }
}
