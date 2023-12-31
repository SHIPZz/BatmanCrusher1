using System;
using UnityEngine;

public class VictoryCanvasEvent : MonoBehaviour
{
    [SerializeField] private ChestStorage _chestStorage;
    [SerializeField] private VictoryView _victoryView;

    private void OnEnable()
    {
        _chestStorage.AllStuffAchieved += OnLevelEnded;
    }

    private void OnDisable()
    {
        _chestStorage.AllStuffAchieved -= OnLevelEnded;
    }

    private void OnLevelEnded(int chestCount)
    {
        VictoryModel victoryModel = new VictoryModel(chestCount);
        _victoryView.Show(new VictoryPresenter(victoryModel));
    }
}