using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerHandler : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private LevelStorage _levelsStorage;

    private int _currentLevel;

    private List<Level> _levels = new List<Level>();

    private void Start()
    {
        _currentLevel = 1;

        _levels = _levelsStorage.GetData();

        DisableLevel();
    }

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(TurnOn);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(TurnOn);
    }

    private void TurnOn()
    {
        _currentLevel++;

        DisableLevel();
    }

    private void DisableLevel()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            if (_levels[i].Index != _currentLevel)
            {
                _levels[i].gameObject.SetActive(false);
            }
            else
            {
                _levels[i].gameObject.SetActive(true);
            }
        }
    }
}