using System;
using UnityEngine;

public enum TouchType
{
    Began,
    Moved,
    Ended,
    None
}

public static class TouchManager
{
    private static TouchType _currentType = TouchType.None;

    public static TouchType CurrentType => _currentType;

    private static Vector2 _currentTouchPosition;
    public static Vector2 CurrentTouchPosition => _currentTouchPosition;

    public static Action<TouchType> OnTouchUpdated;
    private static bool _isLocked;

    public static void LockInput()
    {
        _currentType = TouchType.None;
        _isLocked = true;
    }

    public static void UnlockInput()
    {
        _isLocked = false;
    }

    public static void UpdateTouch()
    {
        if (_isLocked)
            return;

#if UNITY_EDITOR
        HandleMouseInput();
#endif

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        HandleAndroidInput();
#endif
        OnTouchUpdated?.Invoke(_currentType);
    }


#if UNITY_EDITOR
    private static void HandleMouseInput()
    {
        if (_currentType == TouchType.Began || _currentType == TouchType.Began)
        {
            _currentType = TouchType.Moved;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _currentType = TouchType.Began;
        }

        if (_currentType == TouchType.Ended)
        {
            _currentType = TouchType.None;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentType = TouchType.Ended;
        }

        _currentTouchPosition = Input.mousePosition;
    }
#endif

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
    private static void HandleAndroidInput()
    {
        
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _currentType = TouchType.Began;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _currentType = TouchType.Moved;
                    break;
                case TouchPhase.Ended:
                    _currentType = TouchType.Ended;
                    break;
            }

            _currentTouchPosition = touch.position;
        }
        else
        {
            _currentType = TouchType.None;
        }
    }
#endif
}