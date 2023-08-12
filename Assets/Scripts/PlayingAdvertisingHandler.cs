using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayingAdvertisingHandler : MonoBehaviour
{
    [SerializeField] private Button _advertisingVictoryButton;
    [SerializeField] private Button _advertisingDeathButton;

    public event Action Opened;
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
        VideoAd.Show(OnOpenedCallback, null,  OnRewardClosedCallback, OnErrorCallback);
    }

    private void SeeLongAdAfterDeathAd()
    {
        VideoAd.Show(OnOpenedCallback, null,  OnRewardedDeathClosedCallback);
    }

    private void OnErrorCallback(string obj) { }

    private void OnOpenedCallback()
    {
        Opened?.Invoke();
    }

    private void OnRewardClosedCallback()
    {
        RewardedClosed?.Invoke();
    }  
    
    private void OnRewardedDeathClosedCallback()
    {
        RewardedDeathClosed?.Invoke();
    }

    private void OnRewardedCallback()
    {
        // RewardedCallbackPlayed?.Invoke();
    }
}