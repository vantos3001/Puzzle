
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    public const string LEVEL_INDEX_SAVE_KEY = "level_index";
    
    public const string IS_TUTORIAL_FINISHED_SAVE_KEY = "is_tutorial_finished";
    
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
        PlayerPrefs.SetInt(LEVEL_INDEX_SAVE_KEY, levelIndex);
        PlayerPrefs.Save();
    }

    public static int GetInt(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, BoolToInt(value));
        PlayerPrefs.Save();
    }

    public static bool GetBool(string key, bool defaultValue = false)
    {
        var value = PlayerPrefs.GetInt(key, BoolToInt(defaultValue));

        return IntToBool(value);
    }

    private static int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    private static bool IntToBool(int value)
    {
        return value > 0;
    }
}

