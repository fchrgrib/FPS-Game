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

    private GameData _statisticsGameData;
    private const string statisticsProfileId = "3";
    private float _elapsedTime = 0;

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

    private void Update()
    {
        _elapsedTime += Time.unscaledDeltaTime;
        // Debug.Log("Elapsed time: " + _elapsedTime);
        if (_elapsedTime >= 5)
        {
            Debug.Log("Saving statistics");
            _elapsedTime = 0;
            GeneralStatistics liveGeneralStatistics = GeneralStatistics.Instance;
            var statsGameData = dataHandler.Load(statisticsProfileId);
            if (statsGameData == null)
            {
                Debug.Log("Babi NGentot");
                _statisticsGameData = new GameData();
            }
            else
            {
                Debug.Log("Babi");
                _statisticsGameData = statsGameData;
                if (liveGeneralStatistics.ElapsedTime <= _statisticsGameData.Stats.elapsedTime)
                {
                    liveGeneralStatistics.ElapsedTime = _statisticsGameData.Stats.elapsedTime;
                    liveGeneralStatistics.TravelDistance = _statisticsGameData.Stats.travelDistance;
                    liveGeneralStatistics.Accuracy = _statisticsGameData.Stats.accuracy;
                    liveGeneralStatistics.UpdateAccuracy(_statisticsGameData.Stats.totalHitCount, 
                        _statisticsGameData.Stats.totalBulletCount);
                    liveGeneralStatistics.Kill = _statisticsGameData.Stats.totalKillCount;
                    liveGeneralStatistics.Death = _statisticsGameData.Stats.totalDeathCount;
                }
            }
            
            Debug.Log("SAMPE SINI");
            _statisticsGameData.Stats.elapsedTime = liveGeneralStatistics.ElapsedTime;
            _statisticsGameData.Stats.travelDistance = liveGeneralStatistics.TravelDistance;
            _statisticsGameData.Stats.accuracy = liveGeneralStatistics.Accuracy;
            _statisticsGameData.Stats.totalHitCount = liveGeneralStatistics.TotalHitCount;
            _statisticsGameData.Stats.totalBulletCount =
                liveGeneralStatistics.TotalBulletCount;
            _statisticsGameData.Stats.totalKillCount = liveGeneralStatistics.Kill;
            _statisticsGameData.Stats.totalDeathCount = liveGeneralStatistics.Death;
            
            Debug.Log("_statistics game data elapsed time: " + liveGeneralStatistics.ElapsedTime);
            
            dataHandler.Save(_statisticsGameData, statisticsProfileId);
        }
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
        SaveGame();
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
