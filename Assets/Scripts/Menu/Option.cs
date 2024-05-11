using System.Collections;
using System.Collections.Generic;
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

    public void OptionMenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
