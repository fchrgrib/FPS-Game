using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerMoney;
    private SceneHelper SceneHelper;

    private static readonly Dictionary<int, string> CurrentLevelToNextLevelMap = new Dictionary<int, string>
    {
        { 1, "Scenes/Level02/Level 02"},
        { 2, "Scenes/Level03/Level 03"},
        { 3, "Scenes/Level04/Level 04"}
    };

    void Start()
    {
        playerMoney.text = PlayerManager.PlayerMoney.ToString();
        SceneHelper = gameObject.AddComponent<SceneHelper>();
    }

    public void ProceedNextLevelAttacker()
    {
        if (PlayerManager.PlayerMoney < 100)
        {
            playerMoney.color = Color.red;
            return;
        }

        PlayerManager.PlayerMoney -= 100;
        Debug.Log("Bought Attacker Pet. Current Money: " + PlayerManager.PlayerMoney);
        SceneParams.PlayerPet = PlayerManager.ATTACKER_PET;
        DataPersistenceManager.instance.SaveStaticGameData();
        SceneHelper.ProceedNextLevel();
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
        DataPersistenceManager.instance.SaveStaticGameData();
        SceneHelper.ProceedNextLevel();
    }
}