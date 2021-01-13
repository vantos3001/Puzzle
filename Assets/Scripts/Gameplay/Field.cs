using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private const int PIXELS_PER_UNIT = 100;

    private const float CUSTOM_CAMERA_SCALE_FACTOR = 0.87f;
    
    private List<Cell> _cells;
    
    public Vector2 _fieldSize;

    private float _referenceCellWidth = 256f;

    public void InjectCells(Vector2 fieldSize , List<Cell> cells)
    {
        _fieldSize = fieldSize;
        _cells = cells;
        
        UpdateCameraSize();
        UpdateCellPositions();
        UpdateFieldPosition();
    }

    private void Awake()
    {
        EventManager.OnInventoryItemMoveStarted += ShowCellForegrounds;
        EventManager.OnInventoryItemMoveEnded += HideCellForegrounds;
    }

    public Cell GetCell(PointData pointData)
    {
        var cell = _cells.Find(c => c.Data.Coords.X == pointData.X && c.Data.Coords.Y == pointData.Y);

        if (cell == null)
        {
            Debug.LogError("Not found Cell with point = " + pointData);

        }

        return cell;
    }

    private void UpdateCellPositions()
    {
        var cellSize = GetCellSize();
        var halfCellSize = cellSize / 2;

        foreach (var cell in _cells)
        {
            var coords = cell.Data.Coords;
            var posX = coords.X * cellSize + halfCellSize;
            var posY = coords.Y * cellSize + halfCellSize;
            
            cell.transform.position = new Vector3(posX, posY, 0);
        }
    }

    private void UpdateCameraSize()
    {
        var cameraSize = GetCameraSizeByCellWidth();
        Camera.main.orthographicSize = cameraSize;
    }

    private void UpdateFieldPosition()
    {
        transform.position = GetStartPosition();
    }

    private float GetCameraSizeByCellWidth()
    {
        var cellSize = GetCellSize();

        var horizontalCount = _fieldSize.x;
        var horizontalCellsSize = horizontalCount * cellSize;
        
        var size = horizontalCellsSize / Screen.width * Screen.height / 2.0f;
        
        return size / CUSTOM_CAMERA_SCALE_FACTOR;
    }
    
    private Vector3 GetStartPosition()
    {
        var cellSize = GetCellSize();

        var deltaX = cellSize * _fieldSize.x / 2;
        var deltaY = cellSize * _fieldSize.y / 2;

        return new Vector3(-deltaX, -deltaY, 0);
    }

    private float GetCellSize()
    {
        return _referenceCellWidth / PIXELS_PER_UNIT;
    }

    private void ShowCellForegrounds(IDraggable draggable)
    {
        if(draggable == null){return;}
        
        foreach (var cell in _cells)
        {
            cell.UpdateForeground(true, draggable);
        }
    }

    private void HideCellForegrounds()
    {
        foreach (var cell in _cells)
        {
            cell.UpdateForeground(false, null);
        }
    }

    private void OnDestroy()
    {
        EventManager.OnInventoryItemMoveStarted -= ShowCellForegrounds;
        EventManager.OnInventoryItemMoveEnded -= HideCellForegrounds;
    }
}
