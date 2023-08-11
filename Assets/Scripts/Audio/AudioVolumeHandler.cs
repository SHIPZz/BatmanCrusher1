using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeHandler : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audios = new List<AudioSource>();
    [SerializeField] private Slider _slider;
    [SerializeField] private VictoryCanvasEvent _victoryCanvas;
    [SerializeField] private DeathCanvasEventView _deathCanvas;
    [SerializeField] private CanvasGroupAlphaHandler _canvasAlphaState;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private EnemyObjectSpawner[] _spawners;

    public float SliderValue =>
        _slider.value;

    private float _lastVolumeValue;
    private PlayingAdvertisingHandler _playingAdvertisingHandler;

    private void OnEnable()
    {
        // _victoryCanvas.CanvasTurned += StopMusic;
        _deathCanvas.CanvasTurned += StopMusic;
        _slider.onValueChanged.AddListener(OnValueChanged);
        _canvasAlphaState.PlayCanvasDisabled += RestartMusic;
        _claimButton.onClick.AddListener(StopMusic);
        _advertisingButton.onClick.AddListener(StopMusic);

        foreach (var spawner in _spawners)
            spawner.AudiosReceived += FillList;
    }

    private void OnDisable()
    {
        // _victoryCanvas.CanvasTurned -= StopMusic;
        _claimButton.onClick.RemoveListener(StopMusic);
        _advertisingButton.onClick.RemoveListener(StopMusic);
        _deathCanvas.CanvasTurned -= StopMusic;
        _canvasAlphaState.PlayCanvasDisabled -= RestartMusic;
        _slider.onValueChanged.RemoveListener(OnValueChanged);
        _playingAdvertisingHandler.RewardedClosed -= RestartMusic;
        _playingAdvertisingHandler.RewardedDeathClosed -= RestartMusic;
        _playingAdvertisingHandler.Opened -= StopMusic;

        foreach (var spawner in _spawners)
            spawner.AudiosReceived -= FillList;
    }

    public void SetPlayingAdvertisingHandler(PlayingAdvertisingHandler playingAdvertisingHandler)
    {
        _playingAdvertisingHandler = playingAdvertisingHandler;
        _playingAdvertisingHandler.RewardedClosed += RestartMusic;
        _playingAdvertisingHandler.RewardedDeathClosed += RestartMusic;
        _playingAdvertisingHandler.Opened += StopMusic;
    }

    public void SetVolume(float value)
    {
        _lastVolumeValue = value;
        _slider.value = value;
    }

    public void StopMusic()
    {
        _lastVolumeValue = _slider.value;
        ControlVolumeAudios(0);
        DataProvider.Instance.SaveVolume(_slider.value);
    }

    public void RestartMusic()
    {
        Time.timeScale = 1f;
        _slider.value = _lastVolumeValue;
        ControlVolumeAudios(_lastVolumeValue);
        DataProvider.Instance.SaveVolume(_slider.value);
    }

    private void OnValueChanged(float value)
    {
        _slider.value = value;
        ControlVolumeAudios(_slider.value);
        DataProvider.Instance.SaveVolume(_slider.value);
    }

    private void FillList(List<AudioSource> audioSources)
    {
        for (int i = 0; i < audioSources.ToList().Count; i++)
        {
            _audios.Add(audioSources[i]);
        }
    }
    
    private void SetAudioVolume(AudioSource audio, float volume, float duration)
    {
        audio.DOKill(); 

        audio.DOFade(volume, duration).SetUpdate(true);
    }

    private void ControlVolumeAudios(float value)
    {
        foreach (var audio in _audios)
        {
            if (audio == null)
                continue;

            SetAudioVolume(audio, value, 0f);
        }
    }
}