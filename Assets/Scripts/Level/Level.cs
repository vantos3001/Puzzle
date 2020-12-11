

public class Level
{
    private LevelData _levelData;

    private Field _field;

    private Inventory _inventory;
    public Inventory Inventory => _inventory;

    public Level(LevelData levelData)
    {
        _levelData = levelData;
    }

    public void InjectField(Field field)
    {
        _field = field;
    }

    public void InjectInventory(Inventory inventory)
    {
        _inventory = inventory;
    }
}
