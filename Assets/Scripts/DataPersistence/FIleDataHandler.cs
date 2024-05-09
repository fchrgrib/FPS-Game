using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        // base case if the profile id is null return right away
        if (profileId == null)
        {
            return null;
        }
        
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // deserialize the data from json back into C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Delete(string profileID)
    {
        // base case if profile id is null
        if (profileID == null)
        {
            return;
        }
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);

        try
        {
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile but it doesnt exist: " + profileID + " at path: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(" Error occured when trying to delete data from file: " + fullPath + "\n" + e);
        }
    }
    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        
        // Loop over all the directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            
            // defensive programming - check if the data file exists
            // if it doesnt, then this folder isnt a profile and should be skipped 
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("skipping directory when loading all profiles because it does not contain data" + profileId);
                continue;
            }
            // Load the game data for this profile and add it to the dictionary
            GameData profileData = Load(profileId);
            // defensive programming - ensure the profile data isnt null
            // because if it is then something went wrong 
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. profileid: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;
        
        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;
            
            // skip this entry
            if (gameData == null)
            {
                continue;
            }
            // if this is the first data we've come across that exists
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            // if this data is more recent than the last one we found
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDatetime = DateTime.FromBinary(gameData.lastUpdated);
                // the greatest DateTime value is the msot recent
                if (newDatetime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }
    
    public void Save(GameData data, string profileId)
    {
     string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
     try
     {
        // create directory fill will be written if it doesnt already exist
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        
        // serialize the c# game data object into json
        string dataToStore = JsonUtility.ToJson(data, true);
        
        // write the serialized data to the file 
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
     }
     catch (Exception e)
     {
         Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
         
     }
    }
}
