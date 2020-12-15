using System;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<Vector3> _points = new List<Vector3>();
    
    private Vector3 _currentPoint;
    public Vector3 CurrentPoint => _currentPoint;

    private bool _isEnd;
    public Action OnPathEnded;
    
    private int _currentIndex;

    public Path(List<Vector3> points)
    {
        _points = points;
        
        _currentPoint = _points[0];
        _currentIndex = 0;
    }

    public void ChangePoint()
    {
        if (_currentIndex < _points.Count - 1)
        {
            _currentIndex++;
            _currentPoint = _points[_currentIndex];
        }
        else
        {
            if (!_isEnd)
            {
                End();
            }
        }
    }

    private void End()
    {
        _isEnd = true;
        OnPathEnded?.Invoke();
    }
    
}
