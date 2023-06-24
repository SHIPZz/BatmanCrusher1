using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonCanvasState : MonoBehaviour
{
    [SerializeField] private Button _openingLanguagePanelButton;
    [SerializeField] private Button _openingLeaderboardPanelButton;
    [SerializeField] private Button _closeLanguagePanel;
    [SerializeField] private Button _homeLeadboardButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _closingCanvasButton;
    [SerializeField] private Button _closingLeaderboard;
    [SerializeField] private EnemyCountLeaderboard _enemyCountLeaderboard;

    private WaitForSeconds _delay = new(1f);
    private Coroutine _delayCoroutine;

    private void OnEnable()
    {
        _openingLanguagePanelButton.onClick.AddListener(OnOpeningLanguagePanelButtonClicked);
        _openingLeaderboardPanelButton.onClick.AddListener(OnOpeningLeaderboardPanelButtonClicked);
        _closeLanguagePanel.onClick.AddListener(OnClosedLanguagePanel);
        _homeLeadboardButton.onClick.AddListener(OnHomeButtonClicked);
        _closingCanvasButton.onClick.AddListener(OnClosingSettingCanvasButton);
        _closingLeaderboard.onClick.AddListener(OnClosingLeaderboardClicked);
        _enemyCountLeaderboard.DataNotLoaded += EnableButtons;
    }

    private void OnDisable()
    {
        _openingLanguagePanelButton.onClick.RemoveListener(OnOpeningLanguagePanelButtonClicked);
        _openingLeaderboardPanelButton.onClick.RemoveListener(OnOpeningLeaderboardPanelButtonClicked);
        _closeLanguagePanel.onClick.RemoveListener(OnClosedLanguagePanel);
        _enemyCountLeaderboard.DataNotLoaded -= EnableButtons;
        _closingCanvasButton.onClick.RemoveListener(OnClosingSettingCanvasButton);
        _homeLeadboardButton.onClick.RemoveListener(OnHomeButtonClicked);
        _closingLeaderboard.onClick.RemoveListener(OnClosingLeaderboardClicked);
    }

    private void OnClosingLeaderboardClicked()
    {
        EnableButtons();
    }

    private void OnHomeButtonClicked()
    {
        EnableButtons();
    }

    private void EnableButtons()
    {
        _openingLanguagePanelButton.interactable = true;
        _openingLeaderboardPanelButton.interactable = true;
        _slider.interactable = true;
        _closingCanvasButton.interactable = true;
    }

    private void OnClosedLanguagePanel()
    {
        if (_delayCoroutine != null)
            StopCoroutine(_delayCoroutine);

        _delayCoroutine = StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return _delayCoroutine;
        EnableButtons();
    }

    private void OnOpeningLeaderboardPanelButtonClicked()
    {
        _openingLanguagePanelButton.interactable = false;
        _slider.interactable = false;
        _openingLeaderboardPanelButton.interactable = true;

        _closingCanvasButton.interactable = false;
    }

    private void OnOpeningLanguagePanelButtonClicked()
    {
        _openingLanguagePanelButton.interactable = true;
        _slider.interactable = false;
        _openingLeaderboardPanelButton.interactable = false;
        _closingCanvasButton.interactable = false;
    }

    private void OnRateButtonClicked()
    {
        _openingLanguagePanelButton.interactable = false;
        _slider.interactable = false;
        _openingLeaderboardPanelButton.interactable = false;
        //_closingCanvasButton.interactable = false;
    }

    private void OnClosingSettingCanvasButton()
    {
        EnableButtons();
    }
}