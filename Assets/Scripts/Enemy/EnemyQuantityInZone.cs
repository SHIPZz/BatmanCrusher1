using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuantityInZone : MonoBehaviour
{
    [SerializeField] private EnemyQuantity _enemyQuantity;
    [SerializeField] private int _enemyCount;

    public event Action AllRemoved;

    private List<EnemyObjectSpawner> _spawners = new List<EnemyObjectSpawner>();
    private EnemyObjectSpawner _objectSpawner;

    private void Awake()
    {
        _spawners = _enemyQuantity.GetList();
    }

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Destroyed += OnEnemyDead;
        }
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Destroyed -= OnEnemyDead;
        }
    }

    private void OnEnemyDead(EnemyObjectSpawner objectSpawner)
    {
        _spawners.Remove(objectSpawner);

        if (_spawners.Count == _enemyCount)
            AllRemoved?.Invoke();
    }
}