﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private ItemButtons _itemButtons;

    [SerializeField] private Button _tapToStart;

    private void Awake()
    {
        EventManager.OnInventoryItemPlaced += OnOnInventoryItemPlaced;
        _tapToStart.onClick.AddListener(OnTapToStartClicked);
    }

    public void UpdateItemButtons(List<InventoryItem> inventoryItems)
    {
        _itemButtons.UpdateButtons(inventoryItems);
    }

    private void OnOnInventoryItemPlaced()
    {
        if (_itemButtons.IsAllFree())
        {
            _tapToStart.gameObject.SetActive(true);
        }
    }

    private void OnTapToStartClicked()
    {
        _tapToStart.gameObject.SetActive(false);
        EventManager.NotifyTapToStartClicked();
    }

    private void OnDestroy()
    {
        EventManager.OnInventoryItemPlaced -= OnOnInventoryItemPlaced;
        _tapToStart.onClick.RemoveListener(OnTapToStartClicked);
    }
}
