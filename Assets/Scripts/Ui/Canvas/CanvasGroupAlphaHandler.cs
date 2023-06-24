using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupAlphaHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _countCanvas;
    [SerializeField] private CanvasGroup _playCanvas;
    [SerializeField] private CanvasGroup _buttonCanvas;
    [SerializeField] private CanvasGroup _deathCanvas;
    [SerializeField] private CanvasGroup _victoryCanvas;
    [SerializeField] private CanvasGroup _leaderboardCanvas;
    [SerializeField] private CanvasGroup _settingCanvas;
    [SerializeField] private CanvasGroup _menuCanvas;
    [SerializeField] private CanvasGroup _notEnoughMoneyCanvas;
    [SerializeField] private CanvasGroup _loadingCanvas;
    [SerializeField] private EnemyCountLeaderboard _enemyLeaderboard;

    [SerializeField] private DeathCanvasEventView _deathCanvasEventView;
    [SerializeField] private VictoryCanvasEvent _victoryCanvasEventView;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _closeMenuCanvas;
    [SerializeField] private Button _closingSettingButton;
    [SerializeField] private Button _closingMenu;
    [SerializeField] private Button _closingNotEnoughMoney;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _resumptionButton;
    [SerializeField] private Button _homeButton;

    [SerializeField] private Button _openLeaderboard;
    [SerializeField] private LoadingCanvasEvent _loadingCanvasEvent;

    [SerializeField] private Button _advertisingButton;
    [SerializeField] private Button _closingLeaderboardButton;
    private PlayingAdvertisingHandler _playingAdvertisingHandler;

    public event Action PlayCanvasDisabled;

    private void Awake()
    {
        SetInitalActive();

        SetInitialAlpha();
    }

    private void OnEnable()
    {
        _deathCanvasEventView.CanvasTurned += OnDeathCanvasEventViewTurned;
        _victoryCanvasEventView.CanvasTurned += OnVictoryCanvasTurned;
        _stopButton.onClick.AddListener(OnStopButtonClicked);
        _closeMenuCanvas.onClick.AddListener(OnClosingMenuCanvasClicked);
        _settingButton.onClick.AddListener(OnSettingButtonClicked);
        _closingSettingButton.onClick.AddListener(OnClosingSettingButtonClicked);
        _closingMenu.onClick.AddListener(OnClosingMenu);
        _resumptionButton.onClick.AddListener(OnClosingMenu);
        _openLeaderboard.onClick.AddListener(OpenLeadboard);
        _closingNotEnoughMoney.onClick.AddListener(CloseNotEnoughMoneyCanvas);
        _loadingCanvasEvent.Loaded += OnCanvasLoadingEnabled;
        _advertisingButton.onClick.AddListener(EnableHud);
        _closingLeaderboardButton.onClick.AddListener(OnClosingLeaderboardButtonClicked);
        _enemyLeaderboard.DataLoaded += OpenLeadboard;
        Wallet.Instance.MoneyEnough += OnPlayingInputClicked;
        _playingAdvertisingHandler.ShortAdClosed += EnableHud;
    }

    private void OnDisable()
    {
        _victoryCanvasEventView.CanvasTurned -= OnVictoryCanvasTurned;
        _deathCanvasEventView.CanvasTurned -= OnDeathCanvasEventViewTurned;
        _stopButton.onClick.RemoveListener(OnStopButtonClicked);
        _closingSettingButton.onClick.RemoveListener(OnClosingSettingButtonClicked);
        _enemyLeaderboard.DataLoaded -= OpenLeadboard;
        _closeMenuCanvas.onClick.RemoveListener(OnClosingMenuCanvasClicked);
        _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        _closingMenu.onClick.RemoveListener(OnClosingMenu);
        _resumptionButton.onClick.RemoveListener(OnClosingMenu);
        _openLeaderboard.onClick.RemoveListener(OpenLeadboard);
        _closingNotEnoughMoney.onClick.RemoveListener(CloseNotEnoughMoneyCanvas);
        _loadingCanvasEvent.Loaded -= OnCanvasLoadingEnabled;
        _advertisingButton.onClick.RemoveListener(EnableHud);
        _closingLeaderboardButton.onClick.RemoveListener(OnClosingLeaderboardButtonClicked);
        //_playButton.onClick.RemoveListener(OnPlayingInputClicked);
        Wallet.Instance.MoneyEnough -= OnPlayingInputClicked;
    }

    public void SetPlayingAdvertisingHandler(PlayingAdvertisingHandler playingAdvertisingHandler)
    {
        _playingAdvertisingHandler = playingAdvertisingHandler;
        _playingAdvertisingHandler.ShortAdClosed += EnableHud;
    }

    private void OnClosingLeaderboardButtonClicked()
    {
        this.SetActive(_leaderboardCanvas.gameObject, false, 0f);
        _settingCanvas.MoveCanvasAlpha(1, 0);
    }

    private void EnableHud()
    {
        _deathCanvas.MoveCanvasAlpha(0, 1);
        this.SetActive(_deathCanvas.gameObject, false, 1f);

        _buttonCanvas.gameObject.SetActive(true);
        _buttonCanvas.MoveCanvasAlpha(1, 1);

        _countCanvas.gameObject.SetActive(true);
        _countCanvas.MoveCanvasAlpha(1, 1);
    }

    private void OnCanvasLoadingEnabled()
    {
        SetInitalActive();
        _playCanvas.gameObject.SetActive(false);
        _loadingCanvas.gameObject.SetActive(true);
        _loadingCanvas.MoveCanvasAlpha(1, 1f);
        
        StartCoroutine(nameof(DisableLoadingCanvas));
    }

    private IEnumerator DisableLoadingCanvas()
    {
        yield return new WaitForSeconds(2.5f);
        _loadingCanvas.MoveCanvasAlpha(0,0);
    }

    private void CloseNotEnoughMoneyCanvas()
    {
        _notEnoughMoneyCanvas.MoveCanvasAlpha(0, 1);
        _notEnoughMoneyCanvas.gameObject.SetActive(false);
    }

    private void SetInitialAlpha()
    {
        _deathCanvas.alpha = 0f;
        _playCanvas.alpha = 1;
        _countCanvas.alpha = 0;
        _buttonCanvas.alpha = 0;
        _leaderboardCanvas.alpha = 0;
        _settingCanvas.alpha = 0;
        _notEnoughMoneyCanvas.alpha = 0;
        _victoryCanvas.alpha = 0;
        _victoryCanvas.alpha = 0;
        _menuCanvas.alpha = 0;
    }

    private void DisableAll()
    {
        _playCanvas.gameObject.SetActive(false);
        _countCanvas.gameObject.SetActive(false);
        _buttonCanvas.gameObject.SetActive(false);
        _leaderboardCanvas.gameObject.SetActive(false);
        _settingCanvas.gameObject.SetActive(false);
        _deathCanvas.gameObject.SetActive(false);
        _menuCanvas.gameObject.SetActive(false);
        _victoryCanvas.gameObject.SetActive(false);
        _loadingCanvas.gameObject.SetActive(false);
        _notEnoughMoneyCanvas.gameObject.SetActive(false);
        _enemyLeaderboard.gameObject.SetActive(false);
    }

    private void SetInitalActive()
    {
        _playCanvas.gameObject.SetActive(false);
        _countCanvas.gameObject.SetActive(false);
        _buttonCanvas.gameObject.SetActive(false);
        _leaderboardCanvas.gameObject.SetActive(false);
        _settingCanvas.gameObject.SetActive(false);
        _deathCanvas.gameObject.SetActive(false);
        _menuCanvas.gameObject.SetActive(false);
        this.SetActive(_victoryCanvas.gameObject, false, 0);
        _loadingCanvas.gameObject.SetActive(false);
        _notEnoughMoneyCanvas.gameObject.SetActive(false);
    }

    private void OpenLeadboard()
    {
        this.SetActive(_leaderboardCanvas.gameObject, true, 0f);
        SetInitalActive();
        _playCanvas.gameObject.SetActive(false);
        _settingCanvas.gameObject.SetActive(true);
        _settingCanvas.MoveCanvasAlpha(0, 0);
        _leaderboardCanvas.MoveCanvasAlpha(1, 1f);
    }

    private void OnClosingMenu()
    {
        this.SetActive(_menuCanvas.gameObject, false, 0.5f);
        this.SetActive(_buttonCanvas.gameObject, true, 0.5f);
        this.SetActive(_countCanvas.gameObject, true, 0.5f);
        this.SetActive(_notEnoughMoneyCanvas.gameObject, false, 0.5f);

        _menuCanvas.MoveCanvasAlpha(0, 0.5f);
        _buttonCanvas.MoveCanvasAlpha(1, 0.5f);
        _countCanvas.MoveCanvasAlpha(1, 0.5f);
    }

    private void OnClosingSettingButtonClicked()
    {
        this.SetActive(_settingCanvas.gameObject, false, 0.5f);
        this.SetActive(_notEnoughMoneyCanvas.gameObject, false, 0.5f);
        this.SetActive(_buttonCanvas.gameObject, true, 0.5f);
        this.SetActive(_countCanvas.gameObject, true, 0.5f);

        _settingCanvas.MoveCanvasAlpha(0, 0.5f);
        _buttonCanvas.MoveCanvasAlpha(1, 0.5f);
        _countCanvas.MoveCanvasAlpha(1, 0.5f);
    }

    private void OnSettingButtonClicked()
    {
        DisableAll();
        _settingCanvas.gameObject.SetActive(true);

        _settingCanvas.MoveCanvasAlpha(1, 1f);
        _countCanvas.MoveCanvasAlpha(0, 0);
        _buttonCanvas.MoveCanvasAlpha(0, 0);
    }

    private void OnClosingMenuCanvasClicked()
    {
        _countCanvas.alpha = 1;
        _buttonCanvas.alpha = 1;
    }

    private void OnDeathCanvasEventViewTurned()
    {
        DisableAll();

        _deathCanvas.gameObject.SetActive(true);

        _deathCanvas.MoveCanvasAlpha(1, 1f);
    }

    private void OnVictoryCanvasTurned()
    {
        DisableAll();

        _victoryCanvas.gameObject.SetActive(true);

        _victoryCanvas.MoveCanvasAlpha(1, 1f);
    }

    private void OnStopButtonClicked()
    {
        this.SetActive(_countCanvas.gameObject, false, 0.5f);
        this.SetActive(_buttonCanvas.gameObject, false, 0.5f);
        this.SetActive(_menuCanvas.gameObject, true, 0f);
        this.SetActive(_deathCanvas.gameObject, false, 0f);
        this.SetActive(_notEnoughMoneyCanvas.gameObject, false, 0f);

        _countCanvas.MoveCanvasAlpha(0, 0.5f);
        _buttonCanvas.MoveCanvasAlpha(0, 0.5f);
        _menuCanvas.MoveCanvasAlpha(1, 1);
    }

    private void OnPlayingInputClicked(bool isEnough)
    {
        if (!isEnough)
            return;

        this.SetActive(_playCanvas.gameObject, false, 0.5f);
        this.SetActive(_countCanvas.gameObject, true, 0f);
        this.SetActive(_buttonCanvas.gameObject, true, 0f);

        _buttonCanvas.MoveCanvasAlpha(1, 0.5f);
        _countCanvas.MoveCanvasAlpha(1, 0.5f);
        _playCanvas.MoveCanvasAlpha(0, 0.5f);

        PlayCanvasDisabled?.Invoke();
    }
}