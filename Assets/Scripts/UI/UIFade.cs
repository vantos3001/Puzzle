using System;
using Game.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private Image _image;

    public Action OnFadeIn;
    public Action OnFadeOut;

    private void Awake()
    {
        CustomSceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeOut();
    }


    public void FadeIn()
    {
        gameObject.SetActive(true);
        
        var color = _image.color;
        color.a = 1f;
        _image.color = color;
        
        OnFadeIn?.Invoke();
    }

    private void FadeOut()
    {
        var color = _image.color;
        color.a = 0f;
        _image.color = color;
        
        gameObject.SetActive(false);
        OnFadeOut?.Invoke();
    }

    private void OnDestroy()
    {
        CustomSceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
