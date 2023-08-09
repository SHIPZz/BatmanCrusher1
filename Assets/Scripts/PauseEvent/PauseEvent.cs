using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseEvent : MonoBehaviour, IPauseService
{
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private DeathCanvasEventView _deathCanvasEventView;
    [SerializeField] private VictoryCanvasEvent _victoryCanvasEvent;
    [SerializeField] private Button _closingSettingButton;
    [SerializeField] private SliderLoadingEvent _sliderLoadingEvent;
    [SerializeField] private RestartingGameEvent _restartingGameEvent;
    [SerializeField] private Button _resumptionButton;
    [SerializeField] private Button _advertisingButton;

    private void OnEnable()
    {
        _stopButton.onClick.AddListener(SetPause);
        _settingButton.onClick.AddListener(SetPause);
        _deathCanvasEventView.CanvasTurned += SetPause;
        _closingSettingButton.onClick.AddListener(UnPause);
        _resumptionButton.onClick.AddListener(UnPause);
        _sliderLoadingEvent.ValueEnded += UnPause;
        _restartingGameEvent.Reloaded += UnPause;
        _advertisingButton.onClick.AddListener(UnPause);
        
    }

    private void OnDisable()
    {
        _stopButton.onClick.RemoveListener(SetPause);
        _resumptionButton.onClick.RemoveListener(UnPause);
        _settingButton.onClick.RemoveListener(SetPause);
        _deathCanvasEventView.CanvasTurned -= SetPause;
        _sliderLoadingEvent.ValueEnded -= UnPause;
        _restartingGameEvent.Reloaded -= UnPause;
        _closingSettingButton.onClick.RemoveListener(UnPause);
        _advertisingButton.onClick.RemoveListener(UnPause);
    }

    public void UnPause() =>
        Time.timeScale = 1f;

    public void SetPause() =>
        Time.timeScale = 0f;
}