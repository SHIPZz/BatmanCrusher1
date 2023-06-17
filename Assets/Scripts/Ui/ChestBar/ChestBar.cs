using TMPro;
using UnityEngine;

public class ChestBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ChestStorage _chestStorage;

    private int _playerAchievedCount;

    private void Awake()
    {
        _text.text = $"{_playerAchievedCount}/{_chestStorage.NotReachedChests}";
    }

    private void OnEnable()
    {
        _chestStorage.PlayerReached += OnPlayerReached;
    }

    private void OnDisable()
    {
        _chestStorage.PlayerReached -= OnPlayerReached;
    }

    private void OnPlayerReached(int playerReachedChests, int notReachedChests)
    {
        _text.text = $"{playerReachedChests}/{notReachedChests}";
    }
}