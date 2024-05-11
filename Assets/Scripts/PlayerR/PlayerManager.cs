using System;
using Microlight.MicroBar;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, CheatListener, IDataPersistence
{

    public const string ATTACKER_PET = "ATTACKER_PET";
    public const string HEALER_PET = "HEALER_PET";
    public const string NO_PET = "NO_PET";
    
    [SerializeField] private GameObject petHolder;
    [SerializeField] private GameObject attackerPet;
    [SerializeField] private GameObject healerPet;
    [SerializeField] private MicroBar playerHealthBar;
    [SerializeField] private TextMeshProUGUI moneyText;

    private GameObject scenePet;
    private const float MaxHp = 100f;
    private float playerHp = 100f;
    private static int playerMoney = 100;
    
    public static int PlayerMoney
    {
        get => playerMoney;
        set
        {
            Debug.Log("Player money is set to: " + value);
            playerMoney = value;
        }
    }

    public float PlayerDamageMultiplier { get; set; } = 1f;
    public int DamageOrbCount { get; set; } = 0;
    private string currentPet = NO_PET;
    
    public static string CurrentPet { get; set; }

    public float PlayerHp
    {
        get => playerHp;
        set
        {
            Debug.Log("Setting player hp to " + value);
            playerHp = value;
            playerHealthBar.UpdateHealthBar(value);
        }
    }
    
    void Start()
    {
        Debug.Log("Start called");
        EventManager.StartListening("MotherlodeCheat", UpdateMoneyText);
    }

    void OnDestroy()
    {
        EventManager.StopListening("MotherlodeCheat", UpdateMoneyText);
    }
    
    private void UpdateMoneyText()
    {
        Debug.Log("Setting player money to: " + PlayerMoney);
        moneyText.text = PlayerMoney.ToString();
    }

    public void LoadData(GameData data)
    {
        PlayerMoney = data.playerMoney;
        SceneParams.PlayerPet = data.playerPet;
        
        setup();
        Debug.Log("Player data Loaded! " + PlayerMoney);
        playerHealthBar.Initialize(MaxHp);
        PlayerHp = data.playerHealth;
    }

    public void SaveData(GameData data)
    {
        data.playerHealth = PlayerHp;
        data.playerMoney = PlayerMoney;
        data.playerPet = currentPet;
        
        Debug.Log("Player data saved! " + data.playerMoney );
    }

    private void setup()
    {
        moneyText.text = PlayerMoney.ToString();
        Debug.Log("Loading money: " + PlayerMoney);
        
        if (string.IsNullOrEmpty(SceneParams.PlayerPet))
        {
            currentPet = NO_PET;
        }
        else
        {
            currentPet = SceneParams.PlayerPet;
        }
        Destroy(scenePet);

        switch (currentPet)
        {
            case ATTACKER_PET:
                scenePet = Instantiate(attackerPet);
                break;
            case HEALER_PET:
                scenePet = Instantiate(healerPet);
                break;
        }

        if (currentPet != NO_PET)
        {
            scenePet.transform.parent = petHolder.transform;
            scenePet.transform.position = petHolder.transform.position;
        }

        CurrentPet = currentPet;
    }

    public void TakeDamage(float damage)
    {
        this.HandleTakingDamage(
            () =>
            {
                PlayerHp -= damage;
                // change this if necessary
                if (PlayerHp <= 0)
                {
                    // game over
                    //TODO: add animation
                    SceneManager.LoadScene("Scenes/Cutscene/DeathCutScene");
                }
            }
        );
    }
    
}