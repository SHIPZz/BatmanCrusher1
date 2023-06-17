using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransformChangingEvent : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _targetScale;
    [SerializeField] private Button _button;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Change);
    }

    private void Change()
    {
        _transform.gameObject.ChangeScale(_targetScale, _duration);
    }
}