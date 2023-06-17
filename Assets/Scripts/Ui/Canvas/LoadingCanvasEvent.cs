using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasEvent : MonoBehaviour
{
    [SerializeField] private Button _claimButton;
    //[SerializeField] private AdvertisingButton _advertisingButton;
    [SerializeField] private RestartingGameEvent _restartingGame;

    public event Action Loaded;

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Load);
        //_advertisingButton.onClick.AddListener(Load);
        _restartingGame.Reloaded += Load;
    }

    private void OnDisable()
    {
        _claimButton.onClick.RemoveListener(Load);
        //_advertisingButton.onClick.RemoveListener(Load);
        _restartingGame.Reloaded -= Load;
    }

    private void Load()
    {
        // Loaded?.Invoke();
    }
}