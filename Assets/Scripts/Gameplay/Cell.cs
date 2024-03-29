﻿using UnityEngine;

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

    private bool IsEmpty()
    {
        return _item == null;
    }

    private ItemType GetItemType()
    {
        return IsEmpty() ? ItemType.Empty : _item.Data.Type;
    }

    private bool CanDrop(IDraggable draggable, out CraftRecipe recipe)
    {
        recipe = null;
        var itemButton = draggable as ItemButton;

        if (itemButton != null && !_data.IsAlwaysLocked)
        {
            return CraftManager.TryGetCraftRecipe(itemButton.Item.Data.Type,GetItemType(), out recipe);
        }

        return false;
    }

    public void RemoveItem()
    {
        if (!IsEmpty())
        {
            var oldItem = _item;
            _item = null;
            Destroy(oldItem.gameObject);
        }
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
        if (TutorialManager.IsTutorial && !TutorialManager.IsTutorialCell(this))
        {
            return false;
        }
        
        if (CanDrop(draggable, out var recipe))
        {
            var itemButton = draggable as ItemButton;

            if (recipe != null)
            {
                CraftManager.DoCraft(recipe, itemButton.Item.Data,  this);
                return true;
            }
            else
            {
                Debug.LogError("Not found recipe for itemType = " + GetItemType());
            }
        }

        return false;
    }
    
    public void UpdateForegroundHighlight(bool isHighlight, bool isHintActive)
    {
        if (isHintActive)
        {
            _cellForeground.HintHighlight(isHighlight);
        }
        else
        {
            _cellForeground.NoHintHighlight(isHighlight);
        }
    }

    public void UpdateForeground(bool isShow, bool isHintActive, IDraggable draggable)
    {
        if(_cellForeground.IsShow == isShow){return;}

        if (isShow && draggable != null)
        {
            if (isHintActive)
            {
                if (CanDrop(draggable, out _))
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
                _cellForeground.ShowNoHintForeground();
            }
        }
        else
        {
            _cellForeground.HideForeground();
        }
    }
}
