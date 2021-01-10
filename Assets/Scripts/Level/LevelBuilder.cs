using System.Collections.Generic;
using UnityEngine;

//TODO: remove from static
public static class LevelBuilder
{
    public static Level BuildLevel(LevelData levelData)
    {
        var level = new Level(levelData);
        
        var field = SpawnField(levelData.CellDates);
        level.InjectField(field);
        
        var inventory = SpawnInventory(levelData.InventoryItemDates);
        level.InjectInventory(inventory);

        var playerPath = SpawnPath(levelData.Path, field);
        level.InjectPlayerPath(playerPath);

        return level;
    }

    private static Field SpawnField(List<CellData> cellDates)
    {
        var fieldPrefab = DataManager.GetPrefab("Field", "Prefabs");
        var fieldGO = Object.Instantiate(fieldPrefab);
        var cells = new List<Cell>();
        foreach (var data in cellDates)
        {
            var cell = PlacementManager.CreateCell(data.CellPrefab, fieldGO.transform);
            cell.InjectData(data);

            if (data.Item != null)
            {
                cell.SetItem(data.Item, true);
            }
            
            cells.Add(cell);
        }

        var field = fieldGO.GetComponent<Field>();
        field.InjectCells(cells);
        
        return field;
    }

    private static Inventory SpawnInventory(List<InventoryItemData> inventoryItemDates)
    {
        var itemList = new List<InventoryItem>();

        foreach (var data in inventoryItemDates)
        {
            var item = new InventoryItem
            {
                Data = data
            };
            itemList.Add(item);
        }
        
        return new Inventory(itemList);
    }

    private static Path SpawnPath(PathData data, Field field)
    {
        List<Vector3> points = new List<Vector3>();

        foreach (var pointData in data.CellPoints)
        {
            var cell = field.GetCell(pointData);

            if (cell != null)
            {
                points.Add(cell.transform.position);
            }
            else
            {
                Debug.LogError("Not found Cell with point = " + pointData);
            }
        }
        
        return new Path(points);
    }

    public static Player SpawnPlayer(PlayerData playerData, Level level)
    {
        var cell = level.Field.GetCell(level.PlayerStartPoint);
        var player = PlacementManager.CreatePlayer(playerData.PlayerPrefab, cell.transform.position);
        
        player.InjectPath(level.PlayerPath);

        return player;
    }
    
    public static Player SpawnTestPlayer(Level level)
    {
        return SpawnPlayer(GetTestPlayer(), level);
    }

    private static PlayerData GetTestPlayer()
    {
        var data = new PlayerData()
        {
            PlayerPrefab = "DefaultPlayer",
        };

        return data;
    }
}
