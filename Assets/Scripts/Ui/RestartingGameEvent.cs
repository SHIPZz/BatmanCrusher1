using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartingGameEvent : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public event Action Reloaded;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(ReloadGame);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ReloadGame);
    }

    private void ReloadGame()
    {
        Reloaded?.Invoke();
    }
}