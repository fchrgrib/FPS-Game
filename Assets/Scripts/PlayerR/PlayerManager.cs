using Microlight.MicroBar;
using UnityEngine;

public class PlayerManager : MonoBehaviour, CheatListener
{
    [SerializeField] private GameObject attackerPet;
    [SerializeField] private GameObject healerPet;
    [SerializeField] private MicroBar playerHealthBar;

    private const float MaxHp = 100f;
    private float playerHp = 100f;
    public float PlayerDamageMultiplier { get; set; } = 1f;
    public int DamageOrbCount { get; set; } = 0;

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
    }

    // Update is called once per frame
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