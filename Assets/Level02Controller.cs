using TMPro;
using UnityEngine;

//TODO: add game over controller
public class Level02Controller : MonoBehaviour
{
    public int EnemyDeathCount { get; private set; }
    public int EnemyLeaderDeathCount { get; private set; }

    public GameObject finalBox;
    public GameObject finalGate;
    public TMP_Text missionText;
    
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
}
