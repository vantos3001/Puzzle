using System;
using System.Collections;
using Game.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeTime = 1f;

    public Action OnFadeIn;
    public Action OnFadeOut;

    private void Awake()
    {
        CustomSceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartFadeOut();
    }


    public void StartFadeIn()
    {
        gameObject.SetActive(true);
        
        StartCoroutine(FadeIn());
    }
    
    private IEnumerator FadeIn()
    {
        for (float i = 0; i < _fadeTime; i += Time.deltaTime)
        {
            var a = Mathf.Min(1f, i / _fadeTime);
            _image.color = SetAlpha(_image.color, a);
            
            yield return null;
        }
        
        _image.color = SetAlpha(_image.color, 1f);
        
        OnFadeIn?.Invoke();
    }

    private Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (float i = _fadeTime; 0f < i; i -= Time.deltaTime)
        {
            var a = Mathf.Max(0f, i / _fadeTime);
            _image.color = SetAlpha(_image.color, a);
            
            yield return null;
        }
        
        _image.color = SetAlpha(_image.color, 0f);
        
        OnFadeOut?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CustomSceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
