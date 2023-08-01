using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private int _health;

    public event Action<Player> Spawned;

    private GameFactory _gameFactory;
    private Dictionary<int, Func<Transform,Player>> _players;
    private int _playerId;

    private void Start()
    {
        _gameFactory = DependencyContainer.Get<GameFactory>();

        _players = new Dictionary<int, Func<Transform,Player>>()
        {
            { Constant.SpiderManId, transform => _gameFactory.CreateObject(Constant.SpiderManPrefab,transform).GetComponentInChildren<Player>() },
            {Constant.BatmanId, transform => _gameFactory.CreateObject(Constant.BatmanPrefab,transform).GetComponentInChildren<Player>() },
            { Constant.WolverineId, transform => _gameFactory.CreateObject(Constant.WolverinePrefab,transform).GetComponentInChildren<Player>() },
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
        Player player = _players[characterId].Invoke(transform);
        
        // player.transform.position = transform.position;

        Spawned?.Invoke(player);
    }

    private bool IsHealthSet(int value) =>
        _health > 0;
}