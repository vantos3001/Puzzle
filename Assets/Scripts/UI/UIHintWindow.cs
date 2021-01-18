using UnityEngine;
using UnityEngine.UI;

public class UIHintWindow : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private void Awake()
    {
        _yesButton.onClick.AddListener(OnYesButtonClicked);
        _noButton.onClick.AddListener(OnNoButtonClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    } 

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnYesButtonClicked()
    {
        EventManager.NotifyOnHintActivated();
        Close();
    }

    private void OnNoButtonClicked()
    {
        Close();
    }

    private void OnDestroy()
    {
        _yesButton.onClick.RemoveListener(OnYesButtonClicked);
        _noButton.onClick.RemoveListener(OnNoButtonClicked);
    }
}
