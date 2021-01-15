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
            player.Die();
        }
        
        var item = other.gameObject.GetComponent<Item>();
        if (item != null && item.Data.Type == ItemType.Wall)
        {
            _isStop = true;
            Debug.Log("IS WALL");
        }
    }
}
