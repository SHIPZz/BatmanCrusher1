using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisingEvent : MonoBehaviour
{
    [SerializeField] private Button _advertisingButton;

    private void OnEnable()
    {
        _advertisingButton.onClick.AddListener(OnAdvertisingButtonClicked);
    }

    private void OnDisable()
    {
        _advertisingButton.onClick.RemoveListener(OnAdvertisingButtonClicked);
    }

    private void OnAdvertisingButtonClicked()
    {
        
    }

    public void See()
    {
        //InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
    }

    private void OnErrorCallback(string obj)
    {

    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1f;
    }

    private void OnRewardedCallback()
    {

    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
    }
}