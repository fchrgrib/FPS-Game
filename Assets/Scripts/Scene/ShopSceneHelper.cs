using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneHelper : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI playerMoney;
    
    void Start()
    {
        playerMoney.text = PlayerManager.PlayerMoney.ToString();
    }
    
    public void ProceedNextLevelAttacker()
    {
        if (PlayerManager.PlayerMoney < 100)
        {
            playerMoney.color = Color.red;
            return;
        }
        
        PlayerManager.PlayerMoney -= 100;
        SceneParams.PlayerPet = PlayerManager.ATTACKER_PET;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }

    public void ProceedNextLevelHealer()
    {
        if (PlayerManager.PlayerMoney < 100)
        {
            playerMoney.color = Color.red;
            return;
        }
        
        PlayerManager.PlayerMoney -= 100;
        SceneParams.PlayerPet = PlayerManager.HEALER_PET;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }
}
