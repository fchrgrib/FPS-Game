using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public void OptionMenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
