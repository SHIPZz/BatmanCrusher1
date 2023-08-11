using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelStorage _levelStorage;

    private List<Level> _levels = new List<Level>();

    private void Awake()
    {
        _levels = _levelStorage.GetData();
    }

    public int GetActiveLevel()
    {
        foreach (var level in _levels)
        {
            if (level.gameObject.activeSelf)
                return level.Index;
        }

        return 0;
    }

    public void Load(int level)
    {
        foreach (Level lvl in _levels)
        {
            lvl.gameObject.SetActive(lvl.Index == level);
        }
    }
}