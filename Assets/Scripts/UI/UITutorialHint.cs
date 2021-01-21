using System.Collections.Generic;
using UnityEngine;


public class TutorialHintMovement
{
    public Vector3 From;
    public Vector3 To;
    public float Speed;
}

public class UITutorialHint : MonoBehaviour
{
    private const float CHANGE_POINT_DISTANCE = 0.001f;
    
    private List<TutorialHintMovement> _tutorialMovements = new List<TutorialHintMovement>();
    private int _currentTutorialMovementIndex;
    private float _currentTime;

    private bool _isShow;

    public void Show(List<TutorialHintMovement> tutorialMovements)
    {
        _isShow = true;
        _currentTutorialMovementIndex = 0;
        _currentTime = 0f;

        _tutorialMovements = tutorialMovements;
        
        transform.position = tutorialMovements[_currentTutorialMovementIndex].From;
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_isShow)
        {
            UpdateMove(Time.deltaTime);
        }
    }

    private void UpdateMove(float delta)
    {
        var point = _tutorialMovements[_currentTutorialMovementIndex];
        var distance = (point.To - transform.position).magnitude;
        
        if (distance < CHANGE_POINT_DISTANCE)
        {
            ChangeMovement();
            point = _tutorialMovements[_currentTutorialMovementIndex];
            distance = (point.To - transform.position).magnitude;
        }
        
        var deltaDistance = delta * point.Speed;

        var t = deltaDistance / distance;

        transform.position = Vector3.Lerp(transform.position, point.To, t);
    }

    private void ChangeMovement()
    {
        if (_currentTutorialMovementIndex < _tutorialMovements.Count - 1)
        {
            _currentTutorialMovementIndex++;
        }
        else
        {
            _currentTutorialMovementIndex = 0;
        }
    }

    public void Hide()
    {
        _isShow = false;
        gameObject.SetActive(false);
    }
}
