using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuantity : MonoBehaviour
{
    [SerializeField] private List<EnemyObjectSpawner> _objectSpawners = new List<EnemyObjectSpawner>();

    public event Action<int> Removed;

    public event Action AllRemoved;

    private void OnEnable()
    {
        foreach (var spawner in _objectSpawners)
        {
            spawner.Destroyed += OnDestroyed;
        }
    }

    private void OnDisable()
    {
        foreach (var spawner in _objectSpawners)
        {
            spawner.Destroyed -= OnDestroyed;
        }
    }

    public List<EnemyObjectSpawner> GetList()
    {
        var list = new List<EnemyObjectSpawner>();

        foreach (var spawner in _objectSpawners)
        {
            list.Add(spawner);
        }

        return list;
    }

    private void OnDestroyed(EnemyObjectSpawner objectSpawner)
    {
        _objectSpawners.Remove(objectSpawner);

        if (_objectSpawners.Count == 0)
            AllRemoved?.Invoke();

        Removed?.Invoke(_objectSpawners.Count);
    }
}