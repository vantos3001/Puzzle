
using System.Collections.Generic;
using UnityEngine;

public class PointData
{
    private int _x;
    public int X => _x;
    
    private int _y;
    public int Y => _y;

    public PointData(int x, int y)
    {
        _x = x;
        _y = y;
    }
}
