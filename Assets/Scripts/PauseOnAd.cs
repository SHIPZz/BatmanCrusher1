using System.Collections.Generic;
using UnityEngine;

public class PauseOnAd : MonoBehaviour
{
    [SerializeField] private List<PlayingAdvertisingHandler> _advertisingHandlers;

    private void OnEnable()
    {
        _advertisingHandlers.ForEach(x =>
        {
            x.Opened += Pause;
            x.RewardedDeathClosed += UnPause;
            x.RewardedClosed += UnPause;
        });
    }

    private void OnDisable()
    {
        _advertisingHandlers.ForEach(x =>
        {
            x.Opened -= Pause;
            x.RewardedDeathClosed -= UnPause;
            x.RewardedClosed -= UnPause;
        });
    }

    private void UnPause() => 
        Time.timeScale = 1f;

    private void Pause()
    {
        Time.timeScale = 0f;
    }
}