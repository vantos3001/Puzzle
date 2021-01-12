using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class DataManager
{
    private const string INVENTORY_ITEM_ICON_PATH = "InventoryItemIcons";
    private const string CELL_BACKGROUND_PATH = "CellBackgrounds";
    
    private static Dictionary<string, Sprite> _cachedInventoryItemIcons = new Dictionary<string, Sprite>();
    private static Dictionary<string, GameObject> _cachedItemPrefabs = new Dictionary<string, GameObject>();
    
    private static Dictionary<string, Sprite> _cachedCellBackgrounds = new Dictionary<string, Sprite>();
    
    private static Dictionary<string, GameObject> _cachedOtherPrefabs = new Dictionary<string, GameObject>();

    public static Sprite GetSprite(string spriteName, string path, Dictionary<string, Sprite> cachedSprites)
    {
        Sprite sprite = null;

        if (cachedSprites.TryGetValue(spriteName, out sprite))
        {
            return sprite;
        }

        sprite = Resources.Load<Sprite>($"{path}/{spriteName}");

        if (sprite != null)
        {
            cachedSprites.Add(spriteName, sprite);
            return sprite;
        }
        else
        {
            Debug.LogError("Not found sprite with name = " + spriteName + "; Path = " + path);
        }

        return sprite;
    }
    
    public static Sprite GetInventoryItemIcons(string iconName)
    {
        return GetSprite(iconName, INVENTORY_ITEM_ICON_PATH, _cachedInventoryItemIcons);
    }

    public static Sprite GetCellBackground(string backgroundName)
    {
        return GetSprite(backgroundName, CELL_BACKGROUND_PATH, _cachedCellBackgrounds);
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
