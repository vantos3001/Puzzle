using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlacementManager
{
    public static bool Place(InventoryItem item, Vector3 position)
    {
        var itemPrefab = DataManager.GetItemPrefab(item.Data.ItemPrefab);

        if (itemPrefab != null)
        {
            var itemGO = GameObject.Instantiate(itemPrefab);
            itemGO.transform.position = position;
            return true;
        }

        return false;
    }
}
