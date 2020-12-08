using UnityEngine;

public class Ceil : MonoBehaviour, IDroppable
{
    private Item _item;

    public bool IsEmpty()
    {
        return _item == null;
    }

    public bool CanDrop(IDraggable draggable)
    {
        var itemButton = draggable as ItemButton;

        if (itemButton != null)
        {
            if (_item == null)
            {
                return true;
            }
            else
            {
                //TODO: try craft
                //CraftManager.Craft();
            }
        }

        return false;
    }

    public bool TryDrop(IDraggable draggable)
    {
        if (CanDrop(draggable))
        {
            var itemButton = draggable as ItemButton;

            if (PlacementManager.Place(itemButton.Item, transform.position, out Item item))
            {
                _item = item;
                
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
