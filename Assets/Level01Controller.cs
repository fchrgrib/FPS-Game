using UnityEngine;


//TODO: add game over controller
public class Level01Controller : MonoBehaviour, IDataPersistence
{
    public GameObject finalGate;

    public GameObject finalBox;

    public int EnemyDeathCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
    }
    
    void Update()
    {
        if (EnemyDeathCount>=10)
        {
            finalGate.SetActive(false);
            finalBox.SetActive(true);
        }    
    }

    public void LoadData(GameData data)
    {
        if (data.currentLevel > 1)
        {
            return;
        }
        Debug.Log("Loading Level 1");

        EnemyDeathCount = data.currentKillCount;
    }

    public void SaveData(GameData data)
    {
        if (data.currentLevel > 1)
        {
            return;
        }

        data.currentLevel = 1;
        data.currentKillCount = EnemyDeathCount;
    }
}
