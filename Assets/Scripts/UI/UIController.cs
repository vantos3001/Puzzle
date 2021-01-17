using System.Collections.Generic;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private ItemButtons _itemButtons;

    [SerializeField] private Button _tapToStart;
    
    [SerializeField] private Button _winButton;
    [SerializeField] private Button _loseButton;

    [SerializeField] private GameObject _soonNewLevels;

    [SerializeField] private UIHeader _uiHeader;

    private void Awake()
    {
        EventManager.OnInventoryItemPlaced += OnOnInventoryItemPlaced;
        _tapToStart.onClick.AddListener(OnTapToStartClicked);
        
        _winButton.onClick.AddListener(OnWinButtonClicked);
        _loseButton.onClick.AddListener(OnLoseButtonClicked);
        
        _uiHeader.ReloadButton.onClick.AddListener(OnReloadButtonClicked);
    }

    public void UpdateItemButtons(List<InventoryItem> inventoryItems)
    {
        _itemButtons.UpdateButtons(inventoryItems);
    }

    public void ShowWin()
    {
        _winButton.gameObject.SetActive(true);
        
        HideHeader();
    }
    
    private void HideWin()
    {
        _winButton.gameObject.SetActive(false);
    }

    public void ShowLose()
    {
        _loseButton.gameObject.SetActive(true);
        
        HideHeader();
    }
    
    private void HideLose()
    {
        _loseButton.gameObject.SetActive(false);
    }

    private void HideHeader()
    {
        _uiHeader.gameObject.SetActive(false);
    }

    public void ShowSoonNewLevels()
    {
        _soonNewLevels.gameObject.SetActive(true);
    }

    private void OnOnInventoryItemPlaced()
    {
        if (_itemButtons.IsAllFree())
        {
            _tapToStart.gameObject.SetActive(true);
        }
    }

    private void OnTapToStartClicked()
    {
        _tapToStart.gameObject.SetActive(false);
        EventManager.NotifyTapToStartClicked();
    }

    private void OnWinButtonClicked()
    {
        HideWin();
        CustomSceneManager.LoadGameplayScene(true);
    }

    private void OnLoseButtonClicked()
    {
        HideLose();
        CustomSceneManager.LoadGameplayScene(true);
    }

    private void OnReloadButtonClicked()
    {
        HideHeader();
        CustomSceneManager.LoadGameplayScene(true);
    }

    private void OnDestroy()
    {
        EventManager.OnInventoryItemPlaced -= OnOnInventoryItemPlaced;
        _tapToStart.onClick.RemoveListener(OnTapToStartClicked);
        
        _winButton.onClick.RemoveListener(OnWinButtonClicked);
        _loseButton.onClick.RemoveListener(OnLoseButtonClicked);
        
        _uiHeader.ReloadButton.onClick.RemoveListener(OnReloadButtonClicked);
    }
}
