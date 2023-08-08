using Agava.YandexGames;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider
{
    private const string CharactersIdKey = "CharactersId";

    private readonly ISaveService _saveLoadSystem;
    private static DataProvider _instance;

    private GameData _gameData;
    private List<int> _characters;

    public static DataProvider Instance => _instance ??= new DataProvider();

    public event Action<int> ChoosedPlayer;
    public event Action<int> LevelChanged;

    private DataProvider()
    {
        if (PlayerAccount.IsAuthorized)
            _saveLoadSystem = new YandexSaveSystem();
        else
            _saveLoadSystem = new PlayerPrefsSystem();
    }

    public void SaveEnemyCount(int enemyCount)
    {
        _gameData.EnemyCount = enemyCount;
        Leaderboard.SetScore(nameof(EnemyCountLeaderboard), _gameData.EnemyCount);
        _saveLoadSystem.Save(_gameData);
    }

    public void SaveMoney()
    {
        _gameData.Money = Wallet.Instance.GetMoney();
        _saveLoadSystem.Save(_gameData);
    }

    public void SaveLevel(int level)
    {
        _gameData.Level = level;
        _saveLoadSystem.Save(_gameData);
        LevelChanged?.Invoke(_gameData.Level);
    }

    public void SaveVolume(float volume)
    {
        _gameData.Volume = volume;
        _saveLoadSystem.Save(_gameData);
    }

    public void SaveImage(int imageId)
    {
        _gameData.ImageId = imageId;
        _saveLoadSystem.Save(_gameData);
    }

    public void SaveCharacter(int characterId)
    {
        _gameData.ChosenCharacter = characterId;
        ChoosedPlayer?.Invoke(characterId);
        _saveLoadSystem.Save(_gameData);
    }

    public int GetCharacter() =>
        _gameData.ChosenCharacter;

    public int GetImage() =>
        _gameData.ImageId;

    public int GetMoney() =>
        _gameData.Money;

    public float GetVolume() =>
        _gameData.Volume;

    public int GetLevel() =>
        _gameData.Level;

    public int GetEnemyCount() =>
        _gameData.EnemyCount;

    public void ClearData()
    {
        _gameData = new GameData();
        PlayerPrefs.DeleteAll();
        _saveLoadSystem.Save(_gameData);
    }

    public bool IsPlayerPurchased(int playerId)
    {
        Debug.Log(_gameData.CharactersId.Count);
        return _gameData.CharactersId.Contains(playerId);
    }

    public void PurchaseCharacter(int characterId)
    {
        if (!IsPlayerPurchased(characterId))
        {
            _gameData.CharactersId.Add(characterId);

            Debug.Log(_gameData.CharactersId.Count);

            _gameData.CharactersKey = JsonUtility.ToJson(_gameData.CharactersId);

            _saveLoadSystem.Save(_gameData);
        }
    }


    public async void LoadInitialData()
    {
        _gameData = await _saveLoadSystem.Load();

        Debug.Log(_gameData + "- gameData");

        try
        {
            JsonUtility.FromJsonOverwrite(_gameData.CharactersKey, _gameData.CharactersId);
        }
        catch (Exception e)
        {
            Debug.Log("Json error");
            throw;
        }
    }
}