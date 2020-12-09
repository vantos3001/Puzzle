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
    [SerializeField] private UIController UiController;
    [SerializeField] private Player Player;

    private Inventory Inventory = new Inventory();

    private GameState _gameState = GameState.None;

    private void Awake()
    {
        EventManager.OnTapToStartClicked += Play;
        
        UiController.UpdateItemButtons(Inventory.InventoryItems);

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

    private void ChangeState(GameState gameState)
    {
        _gameState = gameState;
    }

    private void OnDestroy()
    {
        EventManager.OnTapToStartClicked -= Play;
    }
}
