using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    
    
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";
        
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";
    public static DataPersistenceManager instance {get ; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one DataPersistenceManager instance. destroy newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is disabled");
        }
        
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        InitializeSelectedProfileId();
    }

    public void ChangeSelectedProfileID(string newProfileId)
    {
        //update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }
    
    public void DeleteProfileData(string profileId)
    {
        // delete the data for for this profile id
        dataHandler.Delete(profileId);
        
        // Initialize selected profile id 
        InitializeSelectedProfileId();
        LoadGame();
    }
    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id to test id");
        }
    }
    public void NewGame()
    {
        this.gameData = new GameData();   
    }
    
    public void LoadGame()
    {
        
        if (disableDataPersistence)
        {
            
            return;
        }
        //  Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);
        
        if (this.gameData == null && initializeDataIfNull) 
        {
            Debug.Log("No data was found, initializing data to defaults");
            NewGame();
            return;
        }
        // if no data can be loaded, dont continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found, initializing data to defaults");
            return;
        }
        //  push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void SaveGame()
    {
        if (disableDataPersistence)
        {
            return;
        }
        
        //pass the data to other scripts so they can update it
        if (this.gameData == null)
        {
            Debug.LogWarning("no data was found");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        // timestamp the data
        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        //Save the data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }
    
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().
            OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
