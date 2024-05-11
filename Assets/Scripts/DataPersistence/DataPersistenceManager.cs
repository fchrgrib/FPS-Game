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
        
    public GameData gameData;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";
    public static DataPersistenceManager instance {get ; private set;}

    private void Awake()
    {
        Debug.Log("SAVE LOCATION: " + Application.persistentDataPath);
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
        this.selectedProfileId = newProfileId;
        LoadGame();
    }
    
    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
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
        gameData.playerName = Option.playerName;
        SceneParams.PlayerPet = "";
        PlayerManager.CurrentPet = "";
        PlayerManager.PlayerMoney = 100;
    }
    
    public void LoadGame()
    {
        if (disableDataPersistence)
        {
            return;
        }
        gameData = dataHandler.Load(selectedProfileId);
        
        if (gameData == null && initializeDataIfNull) 
        {
            Debug.Log("No data was found, initializing data to defaults");
            NewGame();
            return;
        }
        if (gameData == null)
        {
            Debug.Log("No data was found, initializing data to defaults");
            return;
        }

        var dataObjects = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataObjects)
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
        // this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void SaveGame()
    {
        if (disableDataPersistence)
        {
            return;
        }
        
        if (this.gameData == null)
        {
            Debug.LogWarning("no data was found");
            return;
        }
        
        Debug.Log("SAVING GAME");
        var dataObjects = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataObjects)
        {
            Debug.Log("Saving class: " + dataPersistenceObj.GetType().Name);
            dataPersistenceObj.SaveData(gameData);
        }
        SaveStaticGameData();
        Debug.Log("SAVING GAME DONE TO MEMORY");
        gameData.lastUpdate = DateTime.Now.ToString();
        
        dataHandler.Save(gameData, selectedProfileId);
    }

    public void SaveStaticGameData()
    {
        gameData.playerMoney = PlayerManager.PlayerMoney;
        gameData.playerPet = SceneParams.PlayerPet;
    }
    
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().
            OfType<IDataPersistence>();
        var res = new List<IDataPersistence>(dataPersistenceObjects);
        Debug.Log("FOUND DATA IN FIND: " + res.Count);

        return res;
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
