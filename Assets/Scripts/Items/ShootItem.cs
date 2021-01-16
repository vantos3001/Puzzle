using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left = 0,
    Right = 1,
    Down = 2,
    Up = 3
}

public class ShootItem : Item
{
    [SerializeField]
    private float ProjectileSpeed = 1f;

    [SerializeField] private float SpawnTime = 1f;

    [SerializeField] private GameObject ProjectilePrefab;

    [SerializeField] private Transform SpawnPoint;

    private Direction _direction = Direction.Down;

    private List<Projectile> Projectiles = new List<Projectile>();

    private float _currentTime = float.MaxValue;

    private bool _isStartShoot;

    protected override void OnDataChanged()
    {
        base.OnDataChanged();

        _direction = Data.Direction;
        RotateToDirection(transform, _direction);
    }

    private void Update()
    {
        if(!_isStartShoot) {return;}
        
        var delta = Time.deltaTime;
        
        UpdateProjectiles(delta);

        _currentTime += delta;

        if (SpawnTime <= _currentTime)
        {
            Shoot();
            _currentTime = 0f;
        }
    }

    private void Awake()
    {
        EventManager.OnTapToStartClicked += OnTapToStartClicked;
    }

    private void OnTapToStartClicked()
    {
        _isStartShoot = true;
    }

    private void Shoot()
    {
        var projGO = Instantiate(ProjectilePrefab);
        projGO.transform.position = SpawnPoint.position;
        RotateToDirection(projGO.transform, _direction);
        Projectiles.Add(projGO.GetComponent<Projectile>());
    }

    private void UpdateProjectiles(float delta)
    {
        List<Projectile> toRemove = new List<Projectile>();
        
        foreach (var projectile in Projectiles)
        {
            var newPos = projectile.transform.position + GetDirection(_direction) * (delta * ProjectileSpeed);
            projectile.transform.position = newPos;

            if (projectile.IsStop || !IsOnScreen(projectile.transform.position))
            {
                toRemove.Add(projectile);
            }
        }

        foreach (var projectile in toRemove)
        {
            Projectiles.Remove(projectile);
            Destroy(projectile.gameObject);
        }
        
        toRemove.Clear();
    }

    private Vector3 GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return new Vector3(0, -1, 0);
            case Direction.Up:
                return new Vector3(0, 1, 0);
            case Direction.Left:
                return new Vector3(-1, 0, 0);
            case Direction.Right:
                return new Vector3(1, 0, 0);
            default:
                throw new Exception("not found projectileDirection = " + direction);
        }
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
                throw new Exception("not found projectileDirection = " + direction);
        }

        var oldEulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(oldEulerAngles.x, oldEulerAngles.y, rotateAngle);
    }

    private bool IsOnScreen(Vector3 position)
    {
        var screenPosition = Camera.main.WorldToScreenPoint(position);

        if (screenPosition.x < 0 || Screen.width < screenPosition.x || screenPosition.y < 0 ||
            Screen.height < screenPosition.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDestroy()
    {
        EventManager.OnTapToStartClicked -= OnTapToStartClicked;
    }
}
