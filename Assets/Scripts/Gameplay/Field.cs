using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private List<Cell> _cells;

    private Vector3 _startPoint = new Vector3(-2.4f, -1.8f);
    private float _cellSize = 0.5f;

    public void InjectCells(List<Cell> cells)
    {
        _cells = cells;
        
        UpdateCellPositions();
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
        foreach (var cell in _cells)
        {
            var coords = cell.Data.Coords;
            var posX = _startPoint.x + coords.X * _cellSize;
            var posY = _startPoint.y + coords.Y * _cellSize;
            
            cell.transform.position = new Vector3(posX, posY, 0);
        }
    }
}
