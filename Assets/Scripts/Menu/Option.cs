using System.Collections;
using System.Collections.Generic;
using Nightmare.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public static string playerName = "default";

    public void changePlayerName(TMP_InputField tmpInputField)
    {
        playerName = tmpInputField.text;
    }

    public void ChangeDifficulty(int diff)
    {
        switch (diff)
        {
            case 0:
                MobSpawner.Difficulty = Difficulty.Easy;
                break;
            case 1:
                MobSpawner.Difficulty = Difficulty.Medium;
                break;
            case 2:
                MobSpawner.Difficulty = Difficulty.Hard;
                break;
            default:
                MobSpawner.Difficulty = Difficulty.Easy;
                break;
        }
    }

    public void OptionMenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
