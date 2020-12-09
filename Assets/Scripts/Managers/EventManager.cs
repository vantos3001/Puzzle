using System;

public static class EventManager
{
    public static Action OnInventoryItemPlaced;
    
    public static Action OnTapToStartClicked;

    public static void NotifyInventoryItemPlaced()
    {
        OnInventoryItemPlaced?.Invoke();
    }

    public static void NotifyTapToStartClicked()
    {
        OnTapToStartClicked?.Invoke();
    }
}
