using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
   
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    
    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;
    

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }
    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
     DisableMenuButtons();   
     
     // update the selected profile id to be used for data persistence
     DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());

     if (!isLoadingGame)
     {
            // Create a new game if there is no data
            DataPersistenceManager.instance.NewGame();
        }
     // save the game anytime before loading a new scene
     DataPersistenceManager.instance.SaveGame();
     //Load the scene - which will trun save the game
     SceneManager.LoadSceneAsync(1);
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
        Activatemenu(isLoadingGame);
    }
    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void Activatemenu(bool isLoadingGame)
    {
        // set this menu to be active
        this.gameObject.SetActive(true);
        // set mode
        this.isLoadingGame = isLoadingGame;
        // Load all profiles
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();
        
        // Loop over all save slots in the ui
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.Setinteractable(false);
            }
            else
            {
                saveSlot.Setinteractable(true);
            }
        }
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.Setinteractable(false);
        }
        backButton.interactable = false;
    }
    
}
