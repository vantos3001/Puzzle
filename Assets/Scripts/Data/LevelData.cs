
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public List<InventoryItemData> InventoryItemDates;

    public Vector2 FieldSize;
    public Vector2 StartFieldPoint;
    public List<CellData> CellDates = new List<CellData>();

    public PointData PlayerStartPoint;
    
    public PathData Path;
}
