using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InterstitialButton : MonoBehaviour
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
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
    }

    private void OnCloseCallback(bool obj)
    {
        SuccessPlayed?.Invoke();
        NotifyClick();
    }

    private void OnErrorCallback(string obj)
    {
        NotifyClick();
    }

    private void NotifyClick()
    {
        Time.timeScale = 1f;
        Clicked?.Invoke();
    }
}