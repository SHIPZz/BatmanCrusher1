using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    
    public event Action<Player> Spawned;

    private GameFactory _gameFactory;
    
    private int _playerId;

    private void Start()
    {
        _gameFactory = DependencyContainer.Get<GameFactory>();
    }

    private void OnEnable()
    {
        DataProvider.Instance.ChoosedPlayer += CreatePlayer;
    }

    private void OnDisable()
    {
        DataProvider.Instance.ChoosedPlayer -= CreatePlayer;
    }

    public void SetCharacterId(int characterId) =>
        _playerId = characterId;

    private void CreatePlayer(int characterId)
    {
        Player player = null;

        if (characterId == Constant.SpiderManId)
        {
            player = _gameFactory.CreateObject(Constant.SpiderPrefab).GetComponentInChildren<Player>();
            
        }
        else if (characterId == Constant.WolverineId)
        {
            player = _gameFactory.CreateObject(Constant.WolverinePrefab).GetComponentInChildren<Player>();
        }
        else if (characterId == Constant.BatmanId)
        {
            player = _gameFactory.CreateObject(Constant.BatmanPrefab).GetComponentInChildren<Player>();
        }

        if (IsHealthSet(_health))
        {
            player.Health.MaxValue = _health;
            player.Health.InitialValue = _health;
        }
        
        // player.transform.parent.SetParent(transform);
        player.transform.position = transform.position;
        
        Spawned?.Invoke(player);
    }

    private bool IsHealthSet(int value) =>
        _health > 0;
}