using System;
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
    private const float INVOKE_RESTART_LEVEL_TIME = 0.35f;
    
    [SerializeField] private UIController UiController;

    private GameState _gameState = GameState.None;

    private Player _player;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        EventManager.OnTapToStartClicked += Play;
        EventManager.OnPlayerPathEnded += Win;
        EventManager.OnPlayerDead += Lose;
        
        UiController.Init();
        

        LoadGame();
    }

    private void LoadGame()
    {
        if (!LevelManager.LoadLevel())
        {
            UiController.ShowSoonNewLevels();
            return;
        }
        
        if (TutorialManager.IsTutorial)
        {
            StartCoroutine(TutorialManager.StartTutorial(UiController));
        }

        var currentLevel = LevelManager.CurrentLevel;
        _player = LevelBuilder.SpawnTestPlayer(currentLevel);
        
        HintManager.Load(currentLevel.IsHintOnStart, LevelManager.CurrentLevelName);
        
        UiController.UpdateItemButtons(currentLevel.Inventory.InventoryItems);

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

            if (!DataManager.GameSettingsConfig.IsTestLevel)
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
            // UiController.ShowLose();
            UiController.HideHeader();
            Invoke(nameof(RestartLevel), INVOKE_RESTART_LEVEL_TIME);
        }
    }

    private void RestartLevel()
    {
        LevelManager.RestartLevel();
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
