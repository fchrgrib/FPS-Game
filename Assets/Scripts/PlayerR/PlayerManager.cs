using Microlight.MicroBar;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, CheatListener
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
    public static int PlayerMoney { get; set; } = 300;
    public float PlayerDamageMultiplier { get; set; } = 1f;
    public int DamageOrbCount { get; set; } = 0;
    private string currentPet = NO_PET;
    
    public static string CurrentPet { get; set; }

    public float PlayerHp
    {
        get => playerHp;
        set
        {
            playerHp = value;
            playerHealthBar.UpdateHealthBar(value);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.Initialize(MaxHp);
        moneyText.text = PlayerMoney.ToString();
        
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
        
        EventManager.StartListening("MotherlodeCheat", UpdateMoneyText);
    }

    void OnDestroy()
    {
        EventManager.StopListening("MotherlodeCheat", UpdateMoneyText);
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
                }
            }
        );
    }
    
    private void UpdateMoneyText()
    {
        moneyText.text = PlayerMoney.ToString();
    }
}