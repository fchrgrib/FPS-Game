using TMPro;
using UnityEngine;

//TODO: add game over controller
public class Level02Controller : MonoBehaviour
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
    }

    private void OnDestroy()
    { 
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
        EventManager.StopListening("LeaderOfEnemyDeath", IncrementLeaderOfEnemyDeathCount);
    }

    string SetTextMission()
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
}
