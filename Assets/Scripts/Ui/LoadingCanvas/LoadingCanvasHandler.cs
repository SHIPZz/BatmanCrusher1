using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingCanvasHandler : MonoBehaviour
{
    [SerializeField] private Button[] _claimButtons;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        foreach (var button in _claimButtons)
        {
            button.onClick.AddListener(Load);
        }
    }

    private void OnDisable()
    {
        foreach (var button in _claimButtons)
        {
            button.onClick.RemoveListener(Load);
        }
        
    }

    private void Load()
    {
        _canvasGroup.MoveCanvasAlpha(1,0.5f);
    }
}
