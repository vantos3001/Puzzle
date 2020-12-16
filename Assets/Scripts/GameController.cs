using UnityEngine;

public enum GameState
{
    None,
    Preparation,
    Play,
    Win,
    Lose
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController UiController;
    [SerializeField] private string TestLevelName;
    
    private GameState _gameState = GameState.None;

    private Level _level;
    private Player _player;

    private void Awake()
    {
        EventManager.OnTapToStartClicked += Play;
        EventManager.OnPlayerPathEnded += Win;
        EventManager.OnPlayerDead += Lose;
        
        LoadGame();
    }

    private void LoadGame()
    {
        var levelData = DataManager.LoadLevelData(TestLevelName);
        _level = LevelBuilder.BuildLevel(levelData);
        
        _player = LevelBuilder.SpawnTestPlayer(_level);
        
        UiController.UpdateItemButtons(_level.Inventory.InventoryItems);


        Prepare();
    }

    private void Prepare()
    {
        if (_gameState != GameState.Play)
        {
            ChangeState(GameState.Preparation);
            _player.Stop();
        }
    }

    private void Play()
    {
        if (_gameState == GameState.Preparation)
        {
            ChangeState(GameState.Play);
            _player.StartMove();
        }
    }

    private void Win()
    {
        if (_gameState == GameState.Play)
        {
            ChangeState(GameState.Win);
            UiController.ShowWin();
        }
    }

    private void Lose()
    {
        if (_gameState == GameState.Play)
        {
            ChangeState(GameState.Lose);
            UiController.ShowLose();
        }
    }

    private void ChangeState(GameState gameState)
    {
        _gameState = gameState;
    }

    private void OnDestroy()
    {
        EventManager.OnTapToStartClicked -= Play;
        EventManager.OnPlayerPathEnded -= Win;
        EventManager.OnPlayerDead -= Lose;
    }
}
