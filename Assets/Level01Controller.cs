using TMPro;
using UnityEngine;


//TODO: add game over controller
public class Level01Controller : MonoBehaviour
{
    public GameObject finalGate;
    public int maxKerocoDeath = 10;

    public GameObject finalBox;
    public TMP_Text text;

    public int EnemyDeathCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("EnemyDeath", IncrementEnemyDeathCount);
        text.SetText(SetTextMission());
    }

    string SetTextMission()
    {
        return $"Your Mission\nKill Keroco     {EnemyDeathCount}/{maxKerocoDeath}";
    }

    private void OnDestroy()
    {
        EventManager.StopListening("EnemyDeath", IncrementEnemyDeathCount);
    }

    private void IncrementEnemyDeathCount()
    {
        EnemyDeathCount++;
        text.SetText(SetTextMission());
    }
    
    void Update()
    {
        if (EnemyDeathCount>=10)
        {
            finalGate.SetActive(false);
            finalBox.SetActive(true);
        }    
    }
}
