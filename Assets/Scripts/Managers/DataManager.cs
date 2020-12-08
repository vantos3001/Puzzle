using System;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static Dictionary<string, Sprite> _cachedInventoryItemIcons = new Dictionary<string, Sprite>();
    private static Dictionary<string, GameObject> _cachedItemPrefabs = new Dictionary<string, GameObject>();
    
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

        prefab = Resources.Load<GameObject>($"ItemPrefabs/{prefabName}");

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
    
}
