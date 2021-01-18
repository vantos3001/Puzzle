using UnityEngine;

public class HintController : MonoBehaviour
{
    private bool _isHintActive;
    public void Load(bool isHintOnStart)
    {
        _isHintActive = isHintOnStart;

        if (_isHintActive)
        {
            StartHint();
        }
        else
        {
            EventManager.OnHintActivated += OnHintActivated;
        }
    }
    
    private void StartHint()
    {
        EventManager.NotifyOnHintStarted();
    }

    private void OnHintActivated()
    {
        EventManager.OnHintActivated -= OnHintActivated;
        
        _isHintActive = true;
        StartHint();
    }

    private void OnDestroy()
    {
        EventManager.OnHintActivated -= OnHintActivated;
    }
}
