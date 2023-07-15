using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public event Action<Player> Spawned;

    private GameFactory _gameFactory;
    private Dictionary<int, Func<Player>> _players;
    private int _playerId;

    private void Start()
    {
        _gameFactory = DependencyContainer.Get<GameFactory>();

        _players = new Dictionary<int, Func<Player>>()
        {
            { Constant.SpiderManId, () => _gameFactory.CreateObject(Constant.SpiderManPrefab).GetComponentInChildren<Player>() },
            {Constant.BatmanId, () => _gameFactory.CreateObject(Constant.BatmanPrefab).GetComponentInChildren<Player>() },
            { Constant.WolverineId, () => _gameFactory.CreateObject(Constant.WolverinePrefab).GetComponentInChildren<Player>() },
        };
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
        Player player = _players[characterId].Invoke();

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