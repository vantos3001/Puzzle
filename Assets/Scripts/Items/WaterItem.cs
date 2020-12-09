using UnityEngine;

public class WaterItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
            Debug.Log("WaterItemWork");
        }
    }
}
