using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderHandler : MonoBehaviour
{
    [SerializeField] private SliderLoadingEvent _sliderLoading;
    [SerializeField] private RestartingGameEvent _restartingGameEvent;
    [SerializeField] private Button _advertisingVictoryButton;
    [SerializeField] private Button _claimButton;

    public event Action SceneStartedLoading;

    private readonly float _delay = 3;
    private readonly int _nextScene = 1;
    private readonly int _allScenes = 14;

    private int _currentScene;

    public int CurrentScene { get; private set; }



    private void OnEnable()
    {
        //_sliderLoading.ValueEnded += LoadNextScene;
        _restartingGameEvent.Reloaded += ReloadScene;
        _advertisingVictoryButton.onClick.AddListener(LoadNextScene);
        _claimButton.onClick.AddListener(LoadNextScene);
    }

    private void OnDisable()
    {
        //_sliderLoading.ValueEnded -= LoadNextScene;
        _advertisingVictoryButton.onClick.RemoveListener(LoadNextScene);
        _claimButton.onClick.RemoveListener(LoadNextScene);
        _restartingGameEvent.Reloaded -= ReloadScene;
    }

    public void SetLevel(int level)
    {
        _currentScene = level;
        CurrentScene = _currentScene;

        //if (_currentScene <= SceneManager.GetActiveScene().buildIndex)
        //    return;

        //SceneManager.LoadScene(_currentScene);
    }

    private void ReloadScene()
    {
        // CurrentScene = _currentScene;
        // DataProvider.Instance.SaveLevel(CurrentScene);
        // SceneStartedLoading?.Invoke();
        //SceneLoaded?.Invoke(_currentScene);
    }

    private void LoadNextScene()
    {
        // if (IsScenesFinished())
        //     return;
        //
        // Debug.Log(_currentScene);
        // CurrentScene = _currentScene + _nextScene;
        // this.Load(_currentScene + _nextScene, _delay);
        // //SceneManager.LoadScene(CurrentScene);
        // DataProvider.Instance.SaveLevel(CurrentScene);
        // SceneStartedLoading?.Invoke();

        //SceneLoaded?.Invoke(_currentScene + _nextScene);
    }

    private bool IsScenesFinished() =>
        _currentScene + _nextScene > _allScenes;
}