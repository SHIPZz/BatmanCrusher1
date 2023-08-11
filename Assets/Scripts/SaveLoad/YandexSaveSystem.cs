using Agava.YandexGames;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class YandexSaveSystem : ISaveService
{
    private GameData _gameData;
    private bool _isSaveDataReceived;

    public void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData);

        PlayerAccount.SetCloudSaveData(data);
    }

    public async UniTask<GameData> Load()
    {
        PlayerAccount.GetCloudSaveData(OnSuccessCallback);

        while (_gameData is null || !_isSaveDataReceived)
        {
            await UniTask.Yield();
        }

        _isSaveDataReceived = false;
        
        return _gameData;
    }

    private GameData ConvertJsonToGameData(string data) =>
        string.IsNullOrEmpty(data) ? new GameData() : JsonUtility.FromJson<GameData>(data);

    private void OnSuccessCallback(string data)
    {
        _gameData = ConvertJsonToGameData(data);
        _isSaveDataReceived = true;
    }
}