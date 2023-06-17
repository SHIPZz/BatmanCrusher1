using DG.Tweening;
using UnityEngine;

public class MusicPlayingEvent : MonoBehaviour
{
    [SerializeField] private CanvasGroupAlphaHandler _canvasState;
    [SerializeField] private DeathCanvasEventView _deathCanvas;
    [SerializeField] private SliderLoadingEvent _sliderLoading;

    private readonly float _duration = 1f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _canvasState.PlayCanvasDisabled += OnCanvasDisabled;
        _deathCanvas.CanvasTurned += DecreaseVolume;
        _sliderLoading.ValueEnded += DecreaseVolume;
    }

    private void OnDisable()
    {
        _canvasState.PlayCanvasDisabled -= OnCanvasDisabled;
        _deathCanvas.CanvasTurned -= DecreaseVolume;
        _sliderLoading.ValueEnded -= DecreaseVolume;
    }

    private void DecreaseVolume()
    {
        _audioSource.DOFade(0, _duration);
    }

    private void OnCanvasDisabled()
    {
        _audioSource.Play();
    }
}