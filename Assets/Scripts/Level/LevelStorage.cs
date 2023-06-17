using System.Collections.Generic;
using UnityEngine;

public class LevelStorage : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();

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
