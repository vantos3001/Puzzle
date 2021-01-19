
public static class HintManager
{
    private static bool _isHintActive;
    public static bool IsHintActive => _isHintActive;

    private static string _levelName;

    public static void Init()
    {
        EventManager.OnHintActivated += OnHintActivated;
    }
    
    public static void Load(bool isHintOnStart, string levelName)
    {
        if (_levelName != levelName)
        {
            _isHintActive = isHintOnStart;
            _levelName = levelName; 
        }

        if (_isHintActive)
        {
            StartHint();
        }
    }
    
    private static void StartHint()
    {
        EventManager.NotifyOnHintStarted();
    }

    private static void OnHintActivated()
    {
        if (!_isHintActive)
        {
            _isHintActive = true;
            StartHint();
        }
    }

    public static void Clear()
    {
        _isHintActive = false;
        _levelName = string.Empty;
    }
}
