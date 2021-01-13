﻿using TMPro;
using UnityEngine;

public class UITextPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText(string text)
    {
        _text.text = text;
    }
}
