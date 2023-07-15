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
    public event Action DeathRewardedCallbackPlayed;

    private void OnEnable()
    {
        _advertisingVictoryButton.onClick.AddListener(SeeLongAd);
        _advertisingDeathButton.onClick.AddListener(SeeLongAdAfterDeathAd);
    }

    private void OnDisable()
    {
        _advertisingVictoryButton.onClick.RemoveListener(SeeLongAd);
        _advertisingDeathButton.onClick.RemoveListener(SeeLongAdAfterDeathAd);
    }

    private void SeeLongAd()
    {
        // RewardedCallbackPlayed?.Invoke();
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
    }

    private void SeeLongAdAfterDeathAd() =>
        VideoAd.Show(null, OnRewardedDealthCallback, OnCloseCallback);

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
        // LongAdClosed?.Invoke();
    }

    private void OnCloseCallback(bool obj)
    {
        Time.timeScale = 1f;
        // DeathRewardedCallbackPlayed?.Invoke();
    }

    private void OnRewardedDealthCallback() =>
        DeathRewardedCallbackPlayed?.Invoke();

    private void OnRewardedCallback()
    {
        RewardedCallbackPlayed?.Invoke();
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
    }
}