using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    private readonly int _targetSize = 50;

    private TextMeshProUGUI _text;
    private int _sceneIndex = 0;

    private void Start()
    {
        GetComponent<I2.Loc.Localize>().enabled = false;

        _text = GetComponent<TextMeshProUGUI>();

        SetInitialSettingText();

        ShowInfo();

        _text.Change();
    }

    public void SetSceneIndex(int sceneIndex) =>
        _sceneIndex = sceneIndex;

    private void SetInitialSettingText()
    {
        if (_sceneIndex == 0)
            _sceneIndex = 1;
        
        _text.text = "";

        _text.fontSize = _targetSize;
    }

    private void ShowInfo()
    {
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            _text.text = $"Уровень {_sceneIndex}";
        }

        if (LocalizationManager.CurrentLanguage == "English")
        {
            _text.text = $"Level {_sceneIndex}";
        }

        if (LocalizationManager.CurrentLanguage == "Turkish")
        {
            _text.text = $"Düzey {_sceneIndex}";
        }

    }
}