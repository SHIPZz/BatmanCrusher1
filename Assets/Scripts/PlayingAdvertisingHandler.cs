using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayingAdvertisingHandler : MonoBehaviour
{
    [SerializeField] private Button _advertisingVictoryButton;
    [SerializeField] private Button _advertisingDeathButton;

    public Button AdvertisingButton =>
        _advertisingVictoryButton;

    public event Action RewardedCallbackPlayed;
    public event Action ShortAdClosed;
    public event Action LongAdClosed;

    private void OnEnable()
    {
        _advertisingVictoryButton.onClick.AddListener(SeeLongAd);
        _advertisingDeathButton.onClick.AddListener(SeeShortAd);
    }

    private void OnDisable()
    {
        _advertisingVictoryButton.onClick.RemoveListener(SeeLongAd);
        _advertisingDeathButton.onClick.RemoveListener(SeeShortAd);
    }

    private void SeeLongAd()
    {
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
    }

    private void SeeShortAd()
    {
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
    }

    private void OnErrorCallback(string obj)
    {
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1f;
        LongAdClosed?.Invoke();
    }

    private void OnCloseCallback(bool obj)
    {
        Time.timeScale = 1f;
        ShortAdClosed?.Invoke();
    }

    private void OnRewardedCallback()
    {
        RewardedCallbackPlayed?.Invoke();
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
    }
}