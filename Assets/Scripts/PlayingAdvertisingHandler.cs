using Agava.YandexGames;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayingAdvertisingHandler : MonoBehaviour
{
    [SerializeField] private Button _advertisingVictoryButton;
    [SerializeField] private Button _advertisingDeathButton;

    public Button AdvertisingButton =>
        _advertisingVictoryButton;

    public event Action RewardedCallbackPlayed;
    public event Action Opened;
    public event Action DeathRewardedCallbackPlayed;
    public event Action RewardedClosed;
    public event Action RewardedDeathClosed;

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
        VideoAd.Show(OnOpenedCallback, OnRewardedCallback, OnRewardClosedCallback, OnErrorCallback);
    }

    private void SeeLongAdAfterDeathAd()
    {
        // Time.timeScale = 1f;
        // Opened?.Invoke();
        //
        // DOTween.Sequence().AppendInterval(0.5f).OnComplete(() =>
        // {
        //     Time.timeScale = 1f;
        //     print("asdasdad");
        //     RewardedDeathClosed?.Invoke();
        // });
        VideoAd.Show(OnOpenedCallback, OnRewardedDealthCallback, OnRewardedDeathClosedCallback);
    }

    private void SeeShortAd()
    {
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
    }

    private void OnErrorCallback(string obj) { }

    private void OnOpenedCallback()
    {
        Opened?.Invoke();
        Time.timeScale = 0f;
    }

    private void OnRewardClosedCallback()
    {
        Time.timeScale = 1f;
        RewardedClosed?.Invoke();
        // LongAdClosed?.Invoke();
    }  
    
    private void OnRewardedDeathClosedCallback()
    {
        Time.timeScale = 1f;
        RewardedDeathClosed?.Invoke();
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