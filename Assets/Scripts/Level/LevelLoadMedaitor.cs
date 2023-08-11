using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using DG.Tweening;
using LvlInit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadMedaitor : MonoBehaviour
{
    [SerializeField] private List<Button> _claimButtons;
    [SerializeField] private List<Button> _restartButtons;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameOverPresenter _gameOverPresenter;
    [SerializeField] private List<LevelInit> _levelInits;

    private readonly int _firstLevel = 1;
    private int _currentLevel;

    private async void Awake()
    {
        foreach (var button in _claimButtons)
        {
            button.onClick.AddListener(LoadNextLevel);
        }

        foreach (var button in _restartButtons)
        {
            button.onClick.AddListener(RestartLevel);
        }

        _gameOverPresenter.CanvasTurned += RestartGame;
    }

    private async void Start()
    {
        await DataProvider.Instance.LoadInitialData();
        
        _currentLevel = DataProvider.Instance.GetLevel();

        if (_currentLevel > 4)
            InterstitialAd.Show();

        _loader.Load(_currentLevel);
        _camera.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (var button in _claimButtons)
        {
            button.onClick.RemoveListener(LoadNextLevel);
        }

        foreach (var button in _restartButtons)
        {
            button.onClick.RemoveListener(RestartLevel);
        }

        _gameOverPresenter.CanvasTurned -= RestartGame;
    }

    private void RestartGame()
    {
        _currentLevel = 1;
        DataProvider.Instance.SaveLevel(_currentLevel);

        foreach (var levelInit in _levelInits)
        {
            levelInit.UploadData();
        }

        DOTween.Sequence().AppendInterval(2f).OnComplete(() => SceneManager.LoadScene(1));
    }

    private void RestartLevel()
    {
        DOTween.Clear();
        SceneManager.LoadScene(1);
    }

    private void LoadNextLevel()
    {
        _currentLevel = DataProvider.Instance.GetLevel();

        _currentLevel++;

        DataProvider.Instance.SaveLevel(_currentLevel);

        _loader.Load(_currentLevel);
    }
}