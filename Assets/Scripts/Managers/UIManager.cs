using UnityEngine;

public static class UIManager
{
    private const string UI_PATH = "Prefabs/UI";
    private const string FADE_NAME = "FadeCanvas";
    
    
    private static bool _isInit;

    private static UIFade _uiFade;
    public static UIFade UiFade => _uiFade;

    public static void Init()
    {
        if (!_isInit)
        {
            var fadeCanvasPrefab = DataManager.Load<GameObject>(UI_PATH, FADE_NAME);
            var fadeCanvasGo = GameObject.Instantiate(fadeCanvasPrefab);
            _uiFade = fadeCanvasGo.GetComponentInChildren<UIFade>();
        }
    }
}
