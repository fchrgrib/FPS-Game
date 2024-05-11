using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Buttons")] [SerializeField]
    private Button backButton;

    private SaveSlot[] saveSlots;
    public static bool IsLoadingGame;
    private static int currentLevel;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        if (IsLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            SceneManager.LoadSceneAsync($"Level 0{DataPersistenceManager.instance.gameData.currentLevel}");
            return;
        }
        
        DisableMenuButtons();
        DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());

        if (!IsLoadingGame)
        {
            DataPersistenceManager.instance.NewGame();
        }

        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("BeginningCutScene");
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
        Start();
    }

    public void OnBackClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Start()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && IsLoadingGame)
            {
                saveSlot.Setinteractable(false);
            }
            else
            {
                saveSlot.Setinteractable(true);
            }
        }
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