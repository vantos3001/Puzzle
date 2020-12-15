using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private List<Cell> _cells;

    private Vector3 _startPoint = Vector3.zero;
    private float _cellSize = 0.5f;

    public List<Cell> Cells => _cells;

    public void InjectCells(List<Cell> cells)
    {
        _cells = cells;
        
        UpdateCellPositions();
    }

    private void UpdateCellPositions()
    {
        foreach (var cell in _cells)
        {
            var coords = cell.Data.Coords;
            var posX = _startPoint.x + coords.X * _cellSize;
            var posY = _startPoint.y + coords.Y * _cellSize;
            
            cell.transform.position = new Vector3(posX, posY, 0);
        }
    }
}
