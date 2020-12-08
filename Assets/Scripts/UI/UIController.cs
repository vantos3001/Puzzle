using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ItemButtons _itemButtons;

    public void UpdateItemButtons(List<InventoryItem> inventoryItems)
    {
        _itemButtons.UpdateButtons(inventoryItems);
    }
}
