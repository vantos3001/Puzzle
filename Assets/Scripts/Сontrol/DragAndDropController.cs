using System;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{

    private IDraggable _current;

    private IDroppable _target;
    
    private bool _isDragging;

    private LayerMask _layerMask;
    
    public void StartDrag(IDraggable draggable, LayerMask layerMask)
    {
        _current = draggable;
        _layerMask = layerMask;
        _target = null;
        _isDragging = true;
    }

    private void Update()
    {
        if (_isDragging)
        {
            UpdateDrag();
        }
    }

    private void UpdateDrag()
    {
        var worldInputPoint = Camera.main.ScreenToWorldPoint(TouchManager.CurrentTouchPosition);
        var raycastHit = Physics2D.Raycast(new Vector2(worldInputPoint.x, worldInputPoint.y), Vector2.zero, 0f, _layerMask);

        IDroppable obj = null;
        
        if (raycastHit.transform != null)
        {
            obj = raycastHit.transform.GetComponent<IDroppable>();
        }
        
        if (_target != obj)
        {
            var oldTarget = _target;
            var newTarget = obj;
            
            UpdateHighlightCellForegrounds(newTarget as Cell, oldTarget as Cell);
            
            Debug.Log("target = " + obj);
            _target = obj;
        }
    }

    private void UpdateHighlightCellForegrounds(Cell newCell, Cell oldCell)
    {
        if (newCell != null)
        {
            newCell.UpdateForegroundHighlight(true, HintManager.IsHintActive);
        }

        if (oldCell != null)
        {
            oldCell.UpdateForegroundHighlight(false, HintManager.IsHintActive);
        }
    }

    public IDroppable EndDrag()
    {
        _current = null;
        _isDragging = false;
        
        UpdateHighlightCellForegrounds(null, _target as Cell);

        return _target;
    }
}
