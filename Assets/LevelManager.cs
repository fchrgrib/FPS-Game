using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IDataPersistence
{

    public static LevelManager currentLevelManager = null;
    
    public int currentLevel;
    private int nextLevel;
    private string nextLevelPath;
    private Scene Scene;
    private SceneHelper SceneHelper;
    [SerializeField] private LayerMask finalFloorMask;

    private void Awake()
    {
        SceneHelper = gameObject.AddComponent<SceneHelper>();
        Scene = SceneManager.GetActiveScene();
        SceneHelper.CurrentScenePath = SceneHelper.GetPathScene(Scene.name);
        if (Scene.name == "Level 04")
        {
            SceneHelper.NextScenePath = SceneHelper.GetPathScene("EndingCutScene");
            return;
        }
        currentLevel = int.Parse(Scene.name.Split(" ")[1]);
        nextLevel = currentLevel + 1;
        SceneHelper.NextScenePath = $"Scenes/Level0{nextLevel}/Level 0{nextLevel}";
        currentLevelManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] isFinalFloor = Physics.OverlapSphere(transform.position, 1f, finalFloorMask);
        Debug.Log(isFinalFloor.Length);
        if (isFinalFloor.Length != 1)
        {
            return;
        }
        NextLevel();
    }

    public void NextLevel()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneHelper.ProceedNextLevel();
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        data.currentLevel = currentLevel;
    }
}
