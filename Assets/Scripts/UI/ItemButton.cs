using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image Icon;

    private InventoryItem _item;

    public void SetContent(InventoryItem item)
    {
        _item = item;
        
        Icon.sprite = DataManager.GetInventoryItemIcons(item.Data.IconName);
    }
    
    public void Clear()
    {
        _item = null;

        Icon.sprite = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_item != null)
        {
            Debug.Log("OnPointerDown");
            //TODO: move sprite
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_item != null)
        {
            Debug.Log("OnPointerUp");

            var worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0;

            if (PlacementManager.Place(_item, worldPosition))
            {
                Clear();
            }
            else
            {
                //TODO: return sprite back
            }
        }
    }
}
