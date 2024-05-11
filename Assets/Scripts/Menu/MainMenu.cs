using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{

    // [Header("Menu Navigation")] 
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        SaveSlotsMenu.IsLoadingGame = false;
        SceneManager.LoadSceneAsync("SaveSlotMenu");
    }

    public void OnLoadGameClicked()
    {
        SaveSlotsMenu.IsLoadingGame = true;
        SceneManager.LoadSceneAsync("SaveSlotMenu");
    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("Continue Game Clicked");
        SceneManager.LoadSceneAsync(4);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
    
    public void OnOptionClicked()
    {
        SceneManager.LoadSceneAsync(2);
    }
    
    public void OnStatsClicked()
    {
        SceneManager.LoadSceneAsync(3);
    }
    
    public void OnBackClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
    }
    
}