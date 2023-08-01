using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agava.WebUtility;
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

    private void Awake()
    {
        DataProvider.Instance.LoadInitialData();

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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        _currentLevel = DataProvider.Instance.GetLevel();

        if (_currentLevel > 3)
            InterstitialAd.Show();

        _loader.Load(_currentLevel);
        _camera.gameObject.SetActive(false);

        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

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


        DOTween.Sequence().AppendInterval(3f).OnComplete(() => _loader.Load(1));

        // _loader.Load(1);
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
        // _loader.Load(_currentLevel);
    }

    private void LoadNextLevel()
    {
        Debug.Log("LOAD!!");

        _currentLevel++;

        Debug.Log(_currentLevel + " LoadNextLevel");

        DataProvider.Instance.SaveLevel(_currentLevel);

        _loader.Load(_currentLevel);
    }
}