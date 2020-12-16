using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class DataManager
{
    private static Dictionary<string, Sprite> _cachedInventoryItemIcons = new Dictionary<string, Sprite>();
    private static Dictionary<string, GameObject> _cachedItemPrefabs = new Dictionary<string, GameObject>();
    
    private static Dictionary<string, GameObject> _cachedOtherPrefabs = new Dictionary<string, GameObject>();
    
    public static Sprite GetInventoryItemIcons(string iconName)
    {
        Sprite icon = null;

        if (_cachedInventoryItemIcons.TryGetValue(iconName, out icon))
        {
            return icon;
        }

        icon = Resources.Load<Sprite>($"InventoryItemIcons/{iconName}");

        if (icon != null)
        {
            _cachedInventoryItemIcons.Add(iconName, icon);
            return icon;
        }
        else
        {
            Debug.LogError("Not found inventoryIcon with name = " + iconName);
        }

        return icon;
    }

    public static GameObject GetItemPrefab(string prefabName)
    {
        GameObject prefab = null;

        if (_cachedItemPrefabs.TryGetValue(prefabName, out prefab))
        {
            return prefab;
        }

        prefab = Resources.Load<GameObject>($"Prefabs/Items/{prefabName}");

        if (prefab != null)
        {
            _cachedItemPrefabs.Add(prefabName, prefab);
            return prefab;
        }
        else
        {
            Debug.LogError("Not found ItemPrefab with name = " + prefabName);
        }

        return prefab;
    }

    public static GameObject GetPrefab(string prefabName, string path)
    {
        GameObject prefab = null;

        if (_cachedOtherPrefabs.TryGetValue(prefabName, out prefab))
        {
            return prefab;
        }

        prefab = Resources.Load<GameObject>($"{path}/{prefabName}");

        if (prefab != null)
        {
            _cachedOtherPrefabs.Add(prefabName, prefab);
            return prefab;
        }
        else
        {
            Debug.LogError("Not found Prefab with name = " + prefabName);
        }

        return prefab;
    }

    public static LevelData LoadLevelData(string levelName)
    {
        var path = $"Assets/Resources/Levels/{levelName}.json";

        LevelData levelData = null;
        
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            levelData = JsonConvert.DeserializeObject<LevelData>(json);

            if (levelData == null)
            {
                Debug.LogError("levelData is null from file = " + levelName);

            }
        }
        else
        {
            
            Debug.Log("Not found Level with name = " + levelName);
        }

        return levelData;
    }
}
