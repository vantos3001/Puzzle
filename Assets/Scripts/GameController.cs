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
    [SerializeField] private bool IsTestLevel = true;
    
    private GameState _gameState = GameState.None;

    private Player _player;

    private void Awake()
    {
        EventManager.OnTapToStartClicked += Play;
        EventManager.OnPlayerPathEnded += Win;
        EventManager.OnPlayerDead += Lose;
        
        CraftManager.Init();
        
        LoadGame();
    }

    private void LoadGame()
    {
        if (IsTestLevel)
        {
            LevelManager.ChangeLevel(TestLevelName);
        }
        else
        {
            LevelManager.ChangeToNextLevel();
        }
        
        _player = LevelBuilder.SpawnTestPlayer(LevelManager.CurrentLevel);
        
        UiController.UpdateItemButtons(LevelManager.CurrentLevel.Inventory.InventoryItems);

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

            if (!IsTestLevel)
            {
                LevelManager.FinishLevel();
            }
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
