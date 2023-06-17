using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private SliderLoadingEvent _sliderLoading;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = $"Loading...{_sliderLoading.Slider.value}%";
    }
}