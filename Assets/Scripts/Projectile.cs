using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool _isStop;

    public bool IsStop
    {
        get => _isStop;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.IsDead = true;
        }
        
        var wall = other.gameObject.GetComponent<WallItem>();
        if (wall != null)
        {
            _isStop = true;
            Debug.Log("IS WALL");
        }
    }
}
