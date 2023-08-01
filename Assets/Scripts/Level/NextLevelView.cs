using I2.Loc;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelView : MonoBehaviour
{
    [SerializeField] private SliderLoadingEvent _loadingEvent;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _text;
    
    public event Action ClaimButtonClicked;
    public event Action RestartButtonClicked;

    public int? Level { get; set; }
    

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(OnAcceptButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _claimButton.onClick.RemoveListener(OnAcceptButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }
    

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();   
    }

    private void SetTextLanguage()
    {
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            _text.text = $"Уровень {Level}";
        }

        if (LocalizationManager.CurrentLanguage == "English")
        {
            _text.text = $"Level {Level}";
        }

        if (LocalizationManager.CurrentLanguage == "Turkish")
        {
            _text.text = $"Düzey {Level}";
        }
    }

    private void OnAcceptButtonClicked()
    {
        ClaimButtonClicked?.Invoke();
    }
}