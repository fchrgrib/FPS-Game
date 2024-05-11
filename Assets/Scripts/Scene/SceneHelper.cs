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

    public void ProceedPreviousLevel()
    {
        SceneParams.PlayerPet = PlayerManager.CurrentPet;
        SceneManager.LoadScene(PreviousScenePath);
    }
}
