using Microlight.MicroBar;
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

    private GameObject scenePet;
    private const float MaxHp = 100f;
    private float playerHp = 100f;
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

    void PlayerOneHitDamage(bool isActive)
    {
        EventManager.TriggerEvent("OneHitDamage", isActive);
    }
    
    // Start is called before the first frame update
    void Start()
    {
            playerHealthBar.Initialize(MaxHp);
        
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
        Debug.Log(CurrentPet);
    }

    void Update()
    {
        // TakeDamage(1f);
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
}