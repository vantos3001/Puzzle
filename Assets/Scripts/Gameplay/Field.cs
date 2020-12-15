using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private List<Cell> _cells;

    public List<Cell> Cells => _cells;

    public void InjectCells(List<Cell> cells)
    {
        _cells = cells;
    }
    
}
