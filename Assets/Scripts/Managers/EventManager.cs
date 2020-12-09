using System;

public static class EventManager
{
    public static Action OnInventoryItemPlaced;
    
    public static Action OnTapToStartClicked;

    public static Action OnPlayerDead;
    public static Action OnPlayerPathEnded;

    public static void NotifyInventoryItemPlaced()
    {
        OnInventoryItemPlaced?.Invoke();
    }

    public static void NotifyTapToStartClicked()
    {
        OnTapToStartClicked?.Invoke();
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
