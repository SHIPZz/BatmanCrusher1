using System;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] private ChestStorage _chestStorage;
    [SerializeField] private CanvasGroup _gameOverCanvas;
    [SerializeField] private CanvasGroup _victoryCanvas;
    [SerializeField] private CanvasGroupAlphaHandler _canvasGroupAlphaHandler;

    public event Action CanvasTurned;

    private void Awake()
    {
        _gameOverCanvas.MoveCanvasAlpha(0, 0);
    }

    private void OnEnable()
    {
        _chestStorage.PlayerAllReached += SetGameOver;
    }

    private void OnDisable()
    {
        _chestStorage.PlayerAllReached -= SetGameOver;
    }

    private void SetGameOver()
    {
        _canvasGroupAlphaHandler.DisableAll();
        _victoryCanvas.gameObject.SetActive(false);
        _gameOverCanvas.gameObject.SetActive(true);
        _gameOverCanvas.MoveCanvasAlpha(1, 1f);
        
        Invoke(nameof(InvokeEvent), 2.5f);
    }

    private void InvokeEvent() =>
        CanvasTurned?.Invoke();
}