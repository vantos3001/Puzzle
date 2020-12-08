using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlacementManager
{
    public static bool Place(InventoryItem inventoryItem, Vector3 position, out Item item)
    {
        var itemPrefab = DataManager.GetItemPrefab(inventoryItem.Data.ItemPrefab);

        if (itemPrefab != null)
        {
            var itemGO = GameObject.Instantiate(itemPrefab);
            itemGO.transform.position = position;
            item = itemGO.GetComponent<Item>();
            return true;
        }

        item = null;

        return false;
    }
}
