using System.Collections.Generic;

public enum InventoryItemType
{
    Wall,
}

public class Inventory
{
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
    public List<InventoryItem> InventoryItems => _inventoryItems;

    public Inventory()
    {
        _inventoryItems = GetTestItems();
    }

    private List<InventoryItem> GetTestItems()
    {
        var inventoryItems = new List<InventoryItem>();
        inventoryItems.Add(new InventoryItem
        {
            Data = new InventoryItemData
            {
                Type = InventoryItemType.Wall,
                IconName = "wall_icon",
                ItemPrefab = "WallItem"
            }
        });
        inventoryItems.Add(new InventoryItem
        {
            Data = new InventoryItemData
            {
                Type = InventoryItemType.Wall,
                IconName = "wall_icon",
                ItemPrefab = "WallItem"
            }
        });

        return inventoryItems;
    }
}
