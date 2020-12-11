using System.Collections.Generic;

public enum InventoryItemType
{
    Wall,
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
