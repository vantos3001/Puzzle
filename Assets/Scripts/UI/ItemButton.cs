using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
{
    [SerializeField] private Image Icon;

    private InventoryItem _item;
    public InventoryItem Item => _item;

    private DragAndDropController _dragAndDropController;

    private void Awake()
    {
        _dragAndDropController = GameObject.FindWithTag("DragAndDropController").GetComponent<DragAndDropController>();
    }

    public void SetContent(InventoryItem item)
    {
        _item = item;
        
        Icon.sprite = DataManager.GetInventoryItemIcons(item.Data.IconName);
    }

    public bool IsItemFree()
    {
        return _item == null || _item.IsFree();
    }
    
    public void Clear()
    {
        if (IsItemFree())
        {
            Icon.sprite = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsItemFree())
        {
            _dragAndDropController.StartDrag(this, LayerMask.GetMask("Field"));
            EventManager.NotifyInventoryItemMoveStarted(this);
            
            Debug.Log("OnPointerDown");
            //TODO: move sprite
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsItemFree())
        {
            Debug.Log("OnPointerUp");

            var worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0;

            var target = _dragAndDropController.EndDrag();
            EventManager.NotifyInventoryItemMoveEnded();

            if (target != null)
            {
                if (target.TryDrop(this))
                {
                    _item.CurrentCount--;
                    Clear();
                    EventManager.NotifyInventoryItemPlaced();
                }
            }
            
            //TODO: return sprite back
        }
    }
}
