using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHeader : MonoBehaviour
{
    [SerializeField] public Button ReloadButton;
    [SerializeField] public Button HintButton;

    [SerializeField] private UIHintWindow _hintWindow;

    public void Init()
    {
        HintButton.onClick.AddListener(OnHintButtonClicked);

        EventManager.OnHintStarted += OnHintStarted;
    }

    private void OnHintButtonClicked()
    {
        _hintWindow.Show();
    }

    private void OnHintStarted()
    {
        HintButton.enabled = false;
    }

    private void OnDestroy()
    {
        HintButton.onClick.RemoveListener(OnHintButtonClicked);
        
        EventManager.OnHintStarted -= OnHintStarted;
    }
}
