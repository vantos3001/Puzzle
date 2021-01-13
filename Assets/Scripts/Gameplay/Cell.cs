using UnityEngine;

public class Cell : MonoBehaviour, IDroppable
{
    [SerializeField] private SpriteRenderer _background;

    [SerializeField] private CellForeground _cellForeground;
        
    public SpriteRenderer Background => _background;
    
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

    public bool SetItem(ItemData itemData, bool force)
    {
        if (_data.IsAlwaysLocked && !force){return false;}
        
        if (PlacementManager.Place(itemData, transform.position, out Item item, transform))
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
            
            return SetItem(itemButton.Item.Data, false);
        }

        return false;
    }

    public void UpdateForeground(bool isShow, IDraggable draggable)
    {
        if(_cellForeground.IsShow == isShow){return;}

        if (isShow && draggable != null)
        {
            if (CanDrop(draggable))
            {
                _cellForeground.ShowUnlockForeground();
            }
            else
            {
                _cellForeground.ShowLockForeground();
            }
        }
        else
        {
            _cellForeground.HideForeground();
        }
    }
}
