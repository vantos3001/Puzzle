using UnityEngine;

public class CellForeground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color UnlockColor;
    [SerializeField] private Color LockColor;

    [SerializeField] private float DefaultAlpha;
    [SerializeField] private float HighlightAlpha;

    private bool _isShow;
    public bool IsShow => _isShow;

    private bool _isHighlight;

    private void Awake()
    {
        UnlockColor.a = DefaultAlpha;
        LockColor.a = DefaultAlpha;
    }

    public void ShowLockForeground()
    {
        ShowForeground();

        _spriteRenderer.color = LockColor;
    }
    
    public void ShowUnlockForeground()
    {
        ShowForeground();

        _spriteRenderer.color = UnlockColor;
    }

    private void ShowForeground()
    {
        if(_isShow) {return;}
        
        _isShow = true;
        _spriteRenderer.enabled = true;
    }

    public void HideForeground()
    {
        if(!_isShow) {return;}
        
        _isShow = false;
        _spriteRenderer.enabled = false;
    }

    public void Highlight(bool isHighlight)
    {
        if(_isHighlight == isHighlight) {return;}
        
        _isHighlight = isHighlight;

        var oldColor = _spriteRenderer.color;
        
        _spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, isHighlight ? HighlightAlpha : DefaultAlpha);
    }
}
