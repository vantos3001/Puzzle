
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    public static void SaveJson(LevelData levelData, string levelName)
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
}

