using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator
{

    public static LevelData CreateTestLevelData()
    {
        return new LevelData
        {
            InventoryItemDates = GetTestItems().ConvertAll(item => item.Data),
            FieldSize = new Vector2(5, 5),
            StartFieldPoint = new Vector2(10, 10),
            CellDates = GetTestCells(),
            PlayerStartPoint = new PointData(0, 4),
            Path = GetTestPath()
        };
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
