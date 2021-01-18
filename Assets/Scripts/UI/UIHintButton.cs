using UnityEngine;
using UnityEngine.UI;

public class UIHintButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    public Button Button => _button;
    
    [SerializeField] private GameObject _activeState;
    [SerializeField] private GameObject _inactiveState;

    public void SetContent(bool isActive)
    {
        if (isActive)
        {
            _activeState.gameObject.SetActive(true);
            
            _inactiveState.gameObject.SetActive(false);
            _button.enabled = false;
        }
        else
        {
            _inactiveState.gameObject.SetActive(true);
            _button.enabled = true;
            
            _activeState.gameObject.SetActive(false);

        }
    }
}
