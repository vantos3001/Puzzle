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

        var playerPath = SpawnPath(levelData.Path, field.Cells);
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
                cell.SetItem(data.Item);
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

    private static Path SpawnPath(PathData data, List<Cell> cells)
    {
        List<Vector3> points = new List<Vector3>();

        foreach (var pointData in data.CellPoints)
        {
            var cell = cells.Find(c => c.Data.Coords.X == pointData.X && c.Data.Coords.Y == pointData.Y);

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

    public static Level BuildTestLevel()
    {
        var data = new LevelData
        {
            InventoryItemDates = GetTestItems().ConvertAll(item => item.Data),
            FieldSize = new Vector2(5, 5),
            StartFieldPoint = new Vector2(10, 10),
            CellDates = GetTestCells(),
            Path = GetTestPath()
        };

        return BuildLevel(data);
    }

    private static List<InventoryItem> GetTestItems()
    {
        var inventoryItems = new List<InventoryItem>();
        inventoryItems.Add(new InventoryItem
        {
            Data = new InventoryItemData
            {
                Type = ItemType.Wall,
                IconName = "wall_icon",
                ItemPrefab = "WallItem"
            }
        });
        inventoryItems.Add(new InventoryItem
        {
            Data = new InventoryItemData
            {
                Type = ItemType.Wall,
                IconName = "wall_icon",
                ItemPrefab = "WallItem"
            }
        });

        return inventoryItems;
    }

    private static List<CellData> GetTestCells()
    {
        var cellDates = new List<CellData>();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var data = new CellData();
                data.Coords = new PointData(i, j);
                data.CellPrefab = "Cell";
                data.Background = "Default";

                if (i == 2 && j == 0)
                {
                    data.Item = new ItemData
                    {
                        ItemPrefab = "WallItem",
                        Type = ItemType.Wall
                    };
                }
                
                if (i == 5 && j == 7)
                {
                    data.Item = new ItemData
                    {
                        ItemPrefab = "ShootItem",
                        Type = ItemType.Shoot
                    };
                }
                
                cellDates.Add(data);
            }
        }

        return cellDates;
    }

    private static PathData GetTestPath()
    {
        var data = new PathData();
        data.CellPoints.Add(new PointData(0, 4));
        data.CellPoints.Add(new PointData(1, 4));
        data.CellPoints.Add(new PointData(2, 4));
        data.CellPoints.Add(new PointData(3, 4));
        data.CellPoints.Add(new PointData(4, 4));
        data.CellPoints.Add(new PointData(5, 4));
        data.CellPoints.Add(new PointData(6, 4));
        data.CellPoints.Add(new PointData(6, 5));
        data.CellPoints.Add(new PointData(7, 5));
        data.CellPoints.Add(new PointData(8, 5));
        data.CellPoints.Add(new PointData(9, 5));

        return data;
    }
}
