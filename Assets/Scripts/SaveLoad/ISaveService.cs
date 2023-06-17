using System.Threading.Tasks;

public interface ISaveService
{
    void Save(GameData gameData);

    Task<GameData> Load();
}