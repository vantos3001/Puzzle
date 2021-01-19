using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float CHANGE_POINT_DISTANCE = 0.001f;

    private Path _path;
    
    [SerializeField] private float Speed = 1;
    [SerializeField] private Sprite DeadSprite;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isMove;

    private bool _isDead;

    private Direction _currentDirection;

    public void InjectPath(Path path)
    {
        if (_path != null)
        {
            _path.OnPathEnded -= OnPathEnded;
        }
        
        _path = path;
        _path.OnPathEnded += OnPathEnded;
        
        UpdateRotation(_path.NextPoint());
    }

    private void Update()
    {
        if (_isMove)
        {
            Move();
        }
    }

    public void Die()
    {
        if(_isDead) return;

        _isDead = true;
        Debug.Log("IS DEAD NOW");

        _spriteRenderer.sprite = DeadSprite;
        transform.eulerAngles = Vector3.zero;

        Stop();
        
        EventManager.NotifyPlayerDead();
    }

    public void Stop()
    {
        _isMove = false;
    }

    public void StartMove()
    {
        _isMove = true;
    }

    private void Move()
    {
        var point = _path.CurrentPoint;
        var distance = (point - transform.position).magnitude;
        
        if (distance < CHANGE_POINT_DISTANCE)
        {
            _path.ChangePoint();
            point = _path.CurrentPoint;
            distance = (point - transform.position).magnitude;
            
            UpdateRotation(_path.CurrentPoint);
        }
        
        var deltaDistance = Time.deltaTime * Speed;

        var t = deltaDistance / distance;

        transform.position = Vector3.Lerp(transform.position, point, t);
    }

    private void UpdateRotation(Vector3 pathPoint)
    {
        _currentDirection = GetDirection(transform.position, pathPoint);
        RotateToDirection(transform, _currentDirection);
    }

    private Direction GetDirection(Vector3 playerPosition, Vector3 point)
    {
        var direction = (point - playerPosition).normalized;

        if (direction.x > 0.9f)
        {
            return Direction.Right;
        }
        
        if(direction.x < -0.9f)
        {
            return Direction.Left;
        }
        
        if (direction.y > 0.9f)
        {
            return Direction.Up;
        }
        
        if (direction.y < -0.9f)
        {
            return Direction.Down;
        }

        return _currentDirection;
    }

    private void RotateToDirection(Transform transform, Direction direction)
    {
        var rotateAngle = 0;

        switch (direction)
        {
            case Direction.Down:
                rotateAngle = -90;
                break;
            case Direction.Up:
                rotateAngle = 90;
                break;
            case Direction.Left:
                rotateAngle = 180;
                break;
            case Direction.Right:
                rotateAngle = 0;
                break;
            default:
                throw new Exception("not found Direction = " + direction);
        }
        
        var oldEulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(oldEulerAngles.x, oldEulerAngles.y, rotateAngle);
    }
    
    private void OnPathEnded()
    {
        Stop();
        EventManager.NotifyPlayerPathEnded();
    }

    private void OnDestroy()
    {
        _path.OnPathEnded -= OnPathEnded;
    }
}
