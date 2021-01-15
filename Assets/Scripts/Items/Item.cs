using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public bool IsKillPlayer;

    private ItemData _data;
    public ItemData Data => _data;

    public void InjectData(ItemData data)
    {
        _data = data;
        
        OnDataChanged();
    }

    protected virtual void OnDataChanged()
    {
        
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (IsKillPlayer)
        {
            TryKillPlayer(other);
        }
    }

    private void TryKillPlayer(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
            Debug.Log("Player killed by item = " + _data.Type);
        }
    }
}
