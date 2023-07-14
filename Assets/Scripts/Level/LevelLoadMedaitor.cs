using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadMedaitor : MonoBehaviour
{
    [SerializeField] private List<NextLevelView> _nextLevelViews;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private List<PlayingAdvertisingHandler> _playingAdvertisingHandlers;
    
    private readonly int _firstLevel = 1;
    private int _currentLevel;

    private void Awake()
    {
        foreach (var view in _nextLevelViews)
        {
            view.ClaimButtonClicked += LoadNextLevelAfterClaimButton;
            view.RestartButtonClicked += RestartGame;
        }
        
        foreach (var playingAdvertisingHandler in _playingAdvertisingHandlers)
        {
            playingAdvertisingHandler.RewardedCallbackPlayed += LoadNextLevelAfterAd;
        }
    }

    private void OnDisable()
    {
        foreach (var view in _nextLevelViews)
        {
            view.ClaimButtonClicked -= LoadNextLevelAfterClaimButton;
            view.RestartButtonClicked -= RestartGame;
        }
        
        foreach (var playingAdvertisingHandler in _playingAdvertisingHandlers)
        {
            playingAdvertisingHandler.RewardedCallbackPlayed -= LoadNextLevelAfterAd;
        }
    }

    public void LoadLevel(int level)
    {
        print(level);
        _currentLevel = level;
        
        foreach (var view in _nextLevelViews)
            view.Level = _currentLevel;
        
        _loader.Load(_currentLevel);
    }

    public void SetLevel(int level)
    {
        foreach (var view in _nextLevelViews)
            view.Level = level;
    }

    private void RestartGame()
    {
        _loader.Load(_currentLevel);
        SceneManager.LoadScene(1);
    }

    private void LoadNextLevelAfterAd()
    {
        LoadNextLevel();
    }
    
    private void LoadNextLevelAfterClaimButton()
    {
        _currentLevel++;
        DataProvider.Instance.SaveLevel(_currentLevel);
        
        SetLevel(_currentLevel);
        
        // SetLevel(_currentLevel++);
        // LoadNextLevel();
        
        // _loader.Load(_currentLevel);

        print("113");
        Invoke(nameof(InvokeLoading), 0.1f);
    }

    private void LoadNextLevel()
    {
        _currentLevel++;
        
        foreach (var view in _nextLevelViews)
            view.Level = _currentLevel;

        DataProvider.Instance.SaveLevel(_currentLevel);
        
        _loader.Load(_currentLevel);
    }

    private void InvokeLoading() =>
        _loader.Load(_currentLevel);
}