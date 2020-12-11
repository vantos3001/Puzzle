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
    [SerializeField] private Player Player;
    
    private GameState _gameState = GameState.None;

    private Level _level;

    private void Awake()
    {
        EventManager.OnTapToStartClicked += Play;
        EventManager.OnPlayerPathEnded += Win;
        EventManager.OnPlayerDead += Lose;
        
        LoadGame();
    }

    private void LoadGame()
    {
        _level = LevelBuilder.BuildTestLevel();
        
        UiController.UpdateItemButtons(_level.Inventory.InventoryItems);

        Prepare();
    }

    private void Prepare()
    {
        if (_gameState != GameState.Play)
        {
            ChangeState(GameState.Preparation);
            Player.Stop();
        }
    }

    private void Play()
    {
        if (_gameState == GameState.Preparation)
        {
            ChangeState(GameState.Play);
            Player.StartMove();
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
