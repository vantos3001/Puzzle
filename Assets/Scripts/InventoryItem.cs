
public class InventoryItem
{
    private InventoryItemData _data;
    public InventoryItemData Data => _data;

    private int _currentCount;
    public int CurrentCount
    {
        get => _currentCount;
        set => _currentCount = value;
    }

    public bool IsFree()
    {
        return _currentCount <= 0;
    }

    public InventoryItem(InventoryItemData data)
    {
        _data = data;
        _currentCount = data.Count;
    }
}
