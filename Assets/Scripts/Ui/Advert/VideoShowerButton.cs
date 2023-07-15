using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoShowerButton : MonoBehaviour
{
    public event Action SuccessPlayed;

    public event Action Clicked;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        SuccessPlayed?.Invoke();
        // NotifyClick();
        // VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
    }

    private void OnRewardedCallback()
    {
        SuccessPlayed?.Invoke();
        Time.timeScale = 1;
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
    }

    private void OnCloseCallback()
    {
        NotifyClick();
    }

    private void OnErrorCallback(string obj)
    {
        NotifyClick();
    }

    private void NotifyClick()
    {
        // Time.timeScale = 1f;
        Clicked?.Invoke();
    }
}