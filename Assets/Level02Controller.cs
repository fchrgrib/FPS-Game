using UnityEngine;

//TODO: add game over controller
public class Level02Controller : MonoBehaviour, IDataPersistence
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }

    public GameObject finalBox;
    public GameObject finalGate;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StartListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
    }

    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
    }

    private void IncrementLeaderOfEnemyDeathCount()
    {
        EnemyLeaderDeathCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLeaderDeathCount >= 3)
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
