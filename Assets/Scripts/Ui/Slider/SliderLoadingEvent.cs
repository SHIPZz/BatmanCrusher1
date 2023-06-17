using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderLoadingEvent : MonoBehaviour
{
    [SerializeField] private LoadingCanvasEvent _loadingCanvas;

    [field: SerializeField] public Slider Slider { get; private set; }

    private const int MaxSliderValue = 100;

    public event Action ValueEnded;

    private readonly float _duration = 2.5f;
    private Coroutine _moveValueCoroutine;

    private void Awake()
    {
        // Slider.interactable = false;
        // Slider.value = 0;
    }

    private void OnEnable()
    {
        _loadingCanvas.Loaded += OnLoaded;
    }

    private void OnDisable()
    {
        _loadingCanvas.Loaded -= OnLoaded;
    }

    private void OnLoaded()
    {
        Slider.DOValue(MaxSliderValue, _duration).SetUpdate(true);
        
        ValueEnded?.Invoke();
        
    }
}