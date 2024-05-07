using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{

    public void ProceedNextLevelAttacker()
    {
        SceneParams.PlayerPet = PlayerManager.ATTACKER_PET;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }

    public void ProceedNextLevelHealer()
    {
        SceneParams.PlayerPet = PlayerManager.HEALER_PET;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }

    public void ProceedNextLevel()
    {
        SceneParams.PlayerPet = PlayerManager.CurrentPet;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }
    
}
