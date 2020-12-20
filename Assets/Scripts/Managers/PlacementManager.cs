using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PlacementManager
{
    public static bool Place(ItemData itemData, Vector3 position, out Item item, Transform parent)
    {
        var itemPrefab = DataManager.GetItemPrefab(itemData.ItemPrefab);

        if (itemPrefab != null)
        {
            var itemGO = GameObject.Instantiate(itemPrefab, parent, true);
            itemGO.transform.position = position;
            item = itemGO.GetComponent<Item>();
            item.InjectData(itemData);
            return true;
        }

        item = null;

        return false;
    }

    public static Cell CreateCell(string cellPrefabName, Transform parent)
    {
        var cellPrefab = DataManager.GetPrefab(cellPrefabName, "Prefabs");

        if (cellPrefab != null)
        {
            var itemGO = Object.Instantiate(cellPrefab, parent, true);
            var cell = itemGO.GetComponent<Cell>();
            return cell;
        }
        
        throw new Exception("Not found cellPrefab = " + cellPrefabName);
    }

    public static Player CreatePlayer(string playerPrefabName, Vector3 position)
    {
        var playerPrefab = DataManager.GetPrefab(playerPrefabName, "Prefabs");

        if (playerPrefab != null)
        {
            var playerGO = Object.Instantiate(playerPrefab);
            playerGO.transform.position = position;
            var player = playerGO.GetComponent<Player>();
            return player;
        }
        
        throw new Exception("Not found playerPrefab = " + playerPrefabName);
    }
}
