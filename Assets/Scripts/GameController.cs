using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController UiController;

    private Inventory Inventory = new Inventory();

    private void Awake()
    {
        UiController.UpdateItemButtons(Inventory.InventoryItems);
    }
}
