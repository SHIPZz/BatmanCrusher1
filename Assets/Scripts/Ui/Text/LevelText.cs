using System;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _level;

    private readonly int _targetSize = 50;
    
    private void Awake()
    {
        GetComponent<I2.Loc.Localize>().enabled = false;
    }

    private void Start()
    {
        SetInitialSettingText();

        ShowInfo();

        _text.Change();
    }

    private void SetInitialSettingText()
    {
        _text.text = "";

        _text.fontSize = _targetSize;
    }


    private void ShowInfo()
    {
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            _text.text = $"Уровень {_level}";
        }

        if (LocalizationManager.CurrentLanguage == "English")
        {
            _text.text = $"Level {_level}";
        }

        if (LocalizationManager.CurrentLanguage == "Turkish")
        {
            _text.text = $"Düzey {_level}";
        }
    }
}