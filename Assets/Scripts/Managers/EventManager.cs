using System;

public static class EventManager
{
    public static Action OnInventoryItemPlaced;
    public static Action<IDraggable> OnInventoryItemMoveStarted;
    public static Action OnInventoryItemMoveEnded;
    
    public static Action OnTapToStartClicked;
    
    public static Action OnHintActivated;
    public static Action OnHintStarted;

    public static Action OnPlayerDead;
    public static Action OnPlayerPathEnded;

    public static void NotifyInventoryItemPlaced()
    {
        OnInventoryItemPlaced?.Invoke();
    }

    public static void NotifyInventoryItemMoveStarted(IDraggable draggable)
    {
        OnInventoryItemMoveStarted?.Invoke(draggable);
    }
    
    public static void NotifyInventoryItemMoveEnded()
    {
        OnInventoryItemMoveEnded?.Invoke();
    }

    public static void NotifyTapToStartClicked()
    {
        OnTapToStartClicked?.Invoke();
    }

    public static void NotifyOnHintActivated()
    {
        OnHintActivated?.Invoke();
    }
    
    public static void NotifyOnHintStarted()
    {
        OnHintStarted?.Invoke();
    }

    public static void NotifyPlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
    
    public static void NotifyPlayerPathEnded()
    {
        OnPlayerPathEnded?.Invoke();
    }
}
