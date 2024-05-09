using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    private int nextLevel;
    private string nextLevelPath;
    private Scene Scene;
    [SerializeField] private LayerMask finalFloorMask;

    private void Awake()
    {
        Scene = SceneManager.GetActiveScene();
        currentLevel = int.Parse(Scene.name.Split(" ")[1]);
        nextLevel = currentLevel + 1;
        nextLevelPath = $"Scenes/Level0{nextLevel}/Level 0{nextLevel}";
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

        if (nextLevel>3)
        {
            //TODO: Do something
            return;
        }
        Debug.Log("Masuk"+nextLevelPath);

        SceneManager.LoadScene(nextLevelPath);
    }
}
