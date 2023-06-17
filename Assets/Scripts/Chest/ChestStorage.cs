using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestStorage : MonoBehaviour
{
    [SerializeField] private List<Chest> _chests = new List<Chest>();

    public event Action<int, int> PlayerReached;
    public event Action<int> AllStuffAchieved;
    public event Action PlayerAllReached;
    public event Action Achieved;

    private int _reachedChests;
    private int _initialCount = 0;

    public int NotReachedChests => _chests.Count;

    private void Awake()
    {
        _initialCount = _chests.Count;
    }

    private void OnEnable() =>
        Chest.Achieved += OnReached;

    private void OnDisable() => 
        Chest.Achieved -= OnReached;

    private void OnReached(Chest chest)
    {
        _reachedChests += 1;

        if (_reachedChests == _initialCount)
        {
            AllStuffAchieved?.Invoke(_initialCount);
            PlayerAllReached?.Invoke();
        }

        _chests.Remove(chest);
        PlayerReached?.Invoke(_reachedChests, _chests.Count);

        Achieved?.Invoke();
    }
}