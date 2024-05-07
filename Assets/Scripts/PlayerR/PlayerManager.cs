using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject attackerPet;
    [SerializeField] private GameObject healerPet;
    [SerializeField] private MicroBar playerHealthBar;

    private const float MaxHp = 100f;
    private float playerHp = 100f;

    public float PlayerHp
    {
        get => playerHp;
        set
        {
            playerHp = value;
            playerHealthBar.UpdateHealthBar(value);
        }
    }
    
    private float playerDamageMultiplier = 1f;

    public float PlayerDamageMultiplier { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.Initialize(MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHp -= 1;
    }
}
