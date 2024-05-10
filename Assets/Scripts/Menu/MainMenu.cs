using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{

    [Header("Menu Navigation")] 
    [SerializeField] private GameObject saveSlotsMenu;

    private SaveSlotsMenu _saveSlotsMenu;
    
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    private void Start()
    {
        // if (DataPersistenceManager.instance.HasGameData()== null)
        // {return;}
        Debug.Log(DataPersistenceManager.instance);
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
            loadGameButton.interactable = false;
        }

        _saveSlotsMenu = saveSlotsMenu.GetComponent<SaveSlotsMenu>();
        Debug.Log("SaveSlotsMenu: " + _saveSlotsMenu);
    }
    public void OnNewGameClicked()
    {
        Debug.Log(saveSlotsMenu);
        _saveSlotsMenu.Activatemenu(false);
        this.DeactivateMenu();
        
        // SceneManager.LoadSceneAsync(4);
    }

    public void OnLoadGameClicked()
    {
        _saveSlotsMenu.Activatemenu(true);
        this.DeactivateMenu();
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

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }
    
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}