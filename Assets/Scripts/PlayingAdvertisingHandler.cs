using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayingAdvertisingHandler : MonoBehaviour
{
    [SerializeField] private Button _advertisingVictoryButton;
    [SerializeField] private Button _advertisingDeathButton;

    public static event Action RewardedCallbackPlayed;
    public static event Action ShortAdClosed;
    public static event Action LongAdClosed;

    private void OnEnable()
    {
        _advertisingVictoryButton.onClick.AddListener(See);
        _advertisingDeathButton.onClick.AddListener(SeeShortAdvert);
    }

    private void OnDisable()
    {
        _advertisingVictoryButton.onClick.RemoveListener(See);
        _advertisingDeathButton.onClick.RemoveListener(SeeShortAdvert);
    }

    public void See()
    {
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
    }

    public void SeeShortAdvert()
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
        //Invoke(nameof(InvokeEvent()))
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

    //private void InvokeEvent(Action action) =>
    //    action?.Invoke();

}