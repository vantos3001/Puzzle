using UnityEngine;

public class Cell : MonoBehaviour, IDroppable
{
    private Item _item;

    private CellData _data;
    public CellData Data => _data;

    public void InjectData(CellData data)
    {
        _data = data;
    }

    public bool IsEmpty()
    {
        return _item == null;
    }

    public bool CanDrop(IDraggable draggable)
    {
        var itemButton = draggable as ItemButton;

        if (itemButton != null)
        {
            if (!_data.IsAlwaysLocked)
            {
                if (IsEmpty()) 
                {
                    return true;
                }
                else
                {
                    //TODO: try craft
                    //CraftManager.Craft();
                }
            }
        }

        return false;
    }

    public bool SetItem(ItemData itemData)
    {
        if (_data.IsAlwaysLocked){return false;}
        
        if (PlacementManager.Place(itemData.ItemPrefab, transform.position, out Item item, transform))
        {
            _item = item;
                
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryDrop(IDraggable draggable)
    {
        if (CanDrop(draggable))
        {
            var itemButton = draggable as ItemButton;
            
            return SetItem(itemButton.Item.Data);
        }

        return false;
    }
}
