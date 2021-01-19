using UnityEngine;

public class CellForeground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color UnlockColor;
    [SerializeField] private Color LockColor;
    
    [SerializeField] private Color NoHintColor;
    
    [SerializeField] private float NoHintDefaultAlpha;
    [SerializeField] private float NoHintHighlightAlpha;

    [SerializeField] private float HintDefaultAlpha;
    [SerializeField] private float HintHighlightAlpha;

    private bool _isShow;
    public bool IsShow => _isShow;

    private bool _isHighlight;

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

    public void ShowNoHintForeground()
    {
        ShowForeground();

        _spriteRenderer.color = NoHintColor;
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

    public void NoHintHighlight(bool isHighlight)
    {
        if(_isHighlight == isHighlight) {return;}
        
        _isHighlight = isHighlight;

        var oldColor = _spriteRenderer.color;
        
        _spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, isHighlight ? NoHintHighlightAlpha : NoHintDefaultAlpha); 
    }

    public void HintHighlight(bool isHighlight)
    {
        if(_isHighlight == isHighlight) {return;}
        
        _isHighlight = isHighlight;

        var oldColor = _spriteRenderer.color;
        
        _spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, isHighlight ? HintHighlightAlpha : HintDefaultAlpha);
    }
}
