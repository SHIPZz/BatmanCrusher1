using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] private ChestStorage _chestStorage;
    [SerializeField] private CanvasGroup _gameOverCanvas;
    [SerializeField] private CanvasGroup _victoryCanvas;
    [SerializeField] private CanvasGroupAlphaHandler _canvasGroupAlphaHandler;

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
        _gameOverCanvas.MoveCanvasAlpha(1, 1f);
    }
}