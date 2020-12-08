using System;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileDirection
{
    Left,
    Right,
    Down,
    Up
}

public class ShootItem : Item
{
    [SerializeField]
    private float ProjectileSpeed = 1f;

    [SerializeField] private float SpawnTime = 1f;

    [SerializeField] private GameObject ProjectilePrefab;

    [SerializeField] private Transform SpawnPoint;

    [SerializeField]
    private ProjectileDirection ProjectileDirection = ProjectileDirection.Down;

    private List<Projectile> Projectiles = new List<Projectile>();

    private float _currentTime = 0f;

    private void Update()
    {
        var delta = Time.deltaTime;
        
        UpdateProjectiles(delta);

        _currentTime += delta;

        if (SpawnTime <= _currentTime)
        {
            Shoot();
            _currentTime = 0f;
        }
    }

    private void Shoot()
    {
        var projGO = Instantiate(ProjectilePrefab);
        projGO.transform.position = SpawnPoint.position;
        Projectiles.Add(projGO.GetComponent<Projectile>());
    }

    private void UpdateProjectiles(float delta)
    {
        List<Projectile> toRemove = new List<Projectile>();
        
        foreach (var projectile in Projectiles)
        {
            var newPos = projectile.transform.position + delta * GetDirection(ProjectileDirection);
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

    private Vector3 GetDirection(ProjectileDirection projectileDirection)
    {
        switch (projectileDirection)
        {
            case ProjectileDirection.Down:
                return new Vector3(0, -1, 0);
            case ProjectileDirection.Up:
                return new Vector3(0, 1, 0);
            case ProjectileDirection.Left:
                return new Vector3(-1, 0, 0);
            case ProjectileDirection.Right:
                return new Vector3(1, 0, 0);
            default:
                throw new Exception("not found projectileDirection = " + projectileDirection);
        }
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
}
