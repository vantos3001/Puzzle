using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHeader : MonoBehaviour
{
    [SerializeField] public Button ReloadButton;
    [SerializeField] private UIHintButton _hintButton;

    [SerializeField] private UIHintWindow _hintWindow;

    public void Init()
    {
        _hintButton.Button.onClick.AddListener(OnHintButtonClicked);
        EventManager.OnHintStarted += OnHintStarted;
    }

    private void OnHintButtonClicked()
    {
        _hintWindow.Show();
    }

    private void OnHintStarted()
    {
        _hintButton.SetContent(true);
    }

    private void OnDestroy()
    {
        _hintButton.Button.onClick.RemoveListener(OnHintButtonClicked);
        
        EventManager.OnHintStarted -= OnHintStarted;
    }
}
