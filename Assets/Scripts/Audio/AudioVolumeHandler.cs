using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeHandler : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audios = new List<AudioSource>();
    [SerializeField] private Slider _slider;
    [SerializeField] private VictoryCanvasEvent _victoryCanvas;
    [SerializeField] private SceneLoaderHandler _sceneLoaderHandler;
    [SerializeField] private DeathCanvasEventView _deathCanvas;
    [SerializeField] private CanvasGroupAlphaHandler _canvasAlphaState;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private PlayingAdvertisingHandler _advertisingHandler;
    [SerializeField] private EnemyObjectSpawner[] _spawners;

    public event Action<float> ValueChanged;

    public float SliderValue =>
        _slider.value;

    private float _lastVolumeValue;
    private PlayingAdvertisingHandler _playingAdvertisingHandler;

    private void OnEnable()
    {
        _victoryCanvas.CanvasTurned += StopMusic;
        _sceneLoaderHandler.SceneStartedLoading += StopMusic;
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
        _victoryCanvas.CanvasTurned -= StopMusic;
        _claimButton.onClick.RemoveListener(StopMusic);
        _playingAdvertisingHandler.ShortAdClosed -= RestartMusic;
        _advertisingButton.onClick.RemoveListener(StopMusic);
        _deathCanvas.CanvasTurned -= StopMusic;
        _canvasAlphaState.PlayCanvasDisabled -= RestartMusic;
        _sceneLoaderHandler.SceneStartedLoading -= StopMusic;
        _slider.onValueChanged.RemoveListener(OnValueChanged);

        foreach (var spawner in _spawners)
            spawner.AudiosReceived -= FillList;
    }

    public void SetPlayingAdvertisingHandler(PlayingAdvertisingHandler playingAdvertisingHandler)
    {
        _playingAdvertisingHandler = playingAdvertisingHandler;
        _playingAdvertisingHandler.ShortAdClosed += RestartMusic;
    }

    public void SetVolume(float value)
    {
        if (value == 0)
            value = 0.1f;

        _lastVolumeValue = value;
        _slider.value = value;
        Debug.Log($"Set - {_slider.value}");
        ControlVolumeAudios(value);
    }

    private void StopMusic()
    {
        _lastVolumeValue = _slider.value;
        ControlVolumeAudios(0);
    }

    private void RestartMusic()
    {
        _slider.value = _lastVolumeValue;
        ControlVolumeAudios(_lastVolumeValue);
        DataProvider.Instance.SaveVolume(_slider.value);
    }

    private void OnValueChanged(float value)
    {
        _slider.value = value;
        ControlVolumeAudios(value);
        DataProvider.Instance.SaveVolume(value);
    }

    private void FillList(List<AudioSource> audioSources)
    {
        for (int i = 0; i < audioSources.ToList().Count; i++)
        {
            _audios.Add(audioSources[i]);
        }
    }

    private void ControlVolumeAudios(float value)
    {
        foreach (var audio in _audios)
        {
            if (audio == null)
                continue;

            audio.volume = value;
        }
    }
}