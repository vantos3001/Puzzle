

public class Level
{
    private LevelData _levelData;

    private Field _field;
    public Field Field => _field;

    private Inventory _inventory;
    public Inventory Inventory => _inventory;

    private Path _playerPath;
    public Path PlayerPath => _playerPath;

    public PointData PlayerStartPoint => _levelData.PlayerStartPoint;

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

    public void InjectPlayerPath(Path playerPath)
    {
        _playerPath = playerPath;
    }
}
