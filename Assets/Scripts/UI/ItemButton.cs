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

    public bool IsFree()
    {
        return _item == null;
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
            _dragAndDropController.StartDrag(this, LayerMask.GetMask("Field"));
            EventManager.NotifyInventoryItemMoveStarted(this);
            
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

            var target = _dragAndDropController.EndDrag();
            EventManager.NotifyInventoryItemMoveEnded();

            if (target != null)
            {
                if (target.TryDrop(this))
                {
                    Clear();
                    EventManager.NotifyInventoryItemPlaced();
                }
            }
            
            //TODO: return sprite back
        }
    }
}
