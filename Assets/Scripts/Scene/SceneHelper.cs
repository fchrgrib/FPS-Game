using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public void ProceedNextLevel()
    {
        SceneParams.PlayerPet = PlayerManager.CurrentPet;
        SceneManager.LoadScene("Scenes/Remade/Main");
    }
}
