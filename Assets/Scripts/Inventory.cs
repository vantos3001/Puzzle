using System.Collections.Generic;

public enum ItemType
{
    Wall = 0,
    Water = 1,
    Shoot = 2,
    Pickaxe = 3,
    Empty = 4,
}

public class Inventory
{
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
    public List<InventoryItem> InventoryItems => _inventoryItems;

    public Inventory(List<InventoryItem> inventoryItems)
    {
        _inventoryItems = inventoryItems;
    }
}
