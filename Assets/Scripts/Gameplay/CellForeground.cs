using UnityEngine;

public class CellForeground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color UnlockColor;
    [SerializeField] private Color LockColor;

    [SerializeField] private float HighlightAlpha;

    private bool _isShow;
    public bool IsShow => _isShow;

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
}
