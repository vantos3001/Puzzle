using System;

[Serializable]
public class CellData
{
    public PointData Coords;
    public string CellPrefab;
    public string Background;

    public bool IsAlwaysLocked;

    public ItemData Item;
}
