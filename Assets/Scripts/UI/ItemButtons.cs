﻿using System.Collections.Generic;
using UnityEngine;

public class ItemButtons : MonoBehaviour
{
    [SerializeField] private List<ItemButton> _itemButtons = new List<ItemButton>();


    public void UpdateButtons(List<InventoryItem> inventoryItems)
    {
        var count = Mathf.Min(inventoryItems.Count, _itemButtons.Count);

        for (int i = 0; i < _itemButtons.Count; i++)
        {
            var button = _itemButtons[i];
            
            if (i < count)
            {
                button.SetContent(inventoryItems[i]);
            }
            else
            {
                button.Clear();
            }
        }
    }

    public bool IsAllFree()
    {
        foreach (var itemButton in _itemButtons)
        {
            if (!itemButton.IsItemFree())
            {
                return false;
            }
        }

        return true;
    }

    public ItemButton GetItemButton(int index)
    {
        return _itemButtons[index];
    }
}
