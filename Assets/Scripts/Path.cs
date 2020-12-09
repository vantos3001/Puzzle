using System;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> Points;

    private Transform _currentPoint;
    public Transform CurrentPoint => _currentPoint;

    private bool _isEnd;
    public Action OnPathEnded;
    
    private int _currentIndex;
    
    private void Start()
    {
        _currentPoint = Points[0];
        _currentIndex = 0;
    }

    public void ChangePoint()
    {
        if (_currentIndex < Points.Count - 1)
        {
            _currentIndex++;
            _currentPoint = Points[_currentIndex];
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
