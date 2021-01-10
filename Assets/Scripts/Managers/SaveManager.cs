
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    public const string SAVE_LEVEL_INDEX_KEY = "level_index";
    
    public static void SaveLevel(LevelData levelData, string levelName)
    {
        string json = JsonConvert.SerializeObject(levelData);

        var path = $"Assets/Resources/Levels/{levelName}.json";

        if (File.Exists(path))
        {
            Debug.LogError("Exist file with name = " + levelName);
        }
        else
        {
            File.WriteAllText(path, json);
            
            Debug.Log("Save level = " + levelName + ": " + json);
        }
    }

    public static void SaveLevelIndex(int levelIndex)
    {
        PlayerPrefs.SetInt(SAVE_LEVEL_INDEX_KEY, levelIndex);
        PlayerPrefs.Save();
    }
}

