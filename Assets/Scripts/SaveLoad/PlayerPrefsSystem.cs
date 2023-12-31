using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerPrefsSystem : ISaveService
{
    private const string DataKey = "GameData";

    private GameData _gameData;

    public void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData);

        PlayerPrefs.SetString(DataKey, data);
        PlayerPrefs.Save();
    }

    public async UniTask<GameData> Load()
    {
        if (PlayerPrefs.HasKey(DataKey))
        {
            string data = PlayerPrefs.GetString(DataKey);
            _gameData = JsonUtility.FromJson<GameData>(data);
            return _gameData;
        }

        await UniTask.Yield();

        return new GameData();
    }
}