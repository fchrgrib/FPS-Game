using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextCrawler : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 75f;
    private SceneHelper SceneHelper;
    private string nameScene;
    private InputManager InputManager;

    private void Awake()
    {
        nameScene = SceneManager.GetActiveScene().name;
        SceneHelper = gameObject.AddComponent<SceneHelper>();
        InputManager = GetComponent<InputManager>();
        SceneHelper.CurrentScenePath = SceneHelper.GetPathScene(nameScene);

        switch (nameScene)
        {
            case "BeginningCutScene":
                SceneHelper.NextScenePath = SceneHelper.GetPathScene("Level 01");
                break;
            default:
                SceneHelper.NextScenePath = SceneHelper.GetPathScene("MainMenu");
                break;
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * scrollSpeed * Time.deltaTime);

        if (transform.position.y >= 4832.717)
        {
            SceneHelper.ProceedNextLevel();
        }
    }

    
}
