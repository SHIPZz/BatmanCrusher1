using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelStorage : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();

    public static LevelStorage Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }

    public int GetLevel(int levelId)
    {
        foreach (var level in _levels)
        {
            if (level.Index == levelId)
                return level.Index;
        }

        return -1;
    }

    public List<Level> GetData()
    {
        var list = new List<Level>();

        for (int i = 0; i < _levels.Count; i++)
        {
            list.Add(_levels[i]);
        }

        return list;
    }
}