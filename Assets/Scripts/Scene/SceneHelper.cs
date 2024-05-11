using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public static string PreviousScenePath;
    public static string CurrentScenePath;
    public static string NextScenePath;
    
    public void ProceedNextLevel()
    {
        SceneParams.PlayerPet = PlayerManager.CurrentPet;
        SceneManager.LoadScene(NextScenePath);
    }

    public void ProceedNextLevelNoChangeToPet()
    {
        SceneManager.LoadScene(NextScenePath);
    }

    public void ProceedPreviousLevel()
    {
        SceneParams.PlayerPet = PlayerManager.CurrentPet;
        SceneManager.LoadScene(PreviousScenePath);
    }

    public string GetPathScene(string nameScene)
    {
        switch (nameScene)
        {
            case "BeginningCutScene":
                return $"Scenes/Cutscene/{nameScene}";
            case "EndingCutScene":
                return $"Scenes/Cutscene/{nameScene}";
            case "DeathCutScene":
                return $"Scenes/Cutscene/{nameScene}";
            case "Level 01":
                return $"Scenes/Level01/{nameScene}";
            case "Level 02":
                return $"Scenes/Level02/{nameScene}";
            case "Level 03":
                return $"Scenes/Level03/{nameScene}";
            case "Level 04":
                return $"Scenes/Level04/{nameScene}";
            case "MainMenu":
                return $"Scenes/Menu/{nameScene}";
            case "LoadGame":
                return $"Scenes/Menu/{nameScene}";
            case "Option":
                return $"Scenes/Menu/{nameScene}";
            case "Statistic":
                return $"Scenes/Menu/{nameScene}";
            case "ShopScene":
                return $"Scenes/Remade/{nameScene}";
            default:
                return "Scenes/Menu/MainMenu";
        }
    }
}
