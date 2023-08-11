using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public interface ISaveService
{
    void Save(GameData gameData);

    UniTask<GameData> Load();
}