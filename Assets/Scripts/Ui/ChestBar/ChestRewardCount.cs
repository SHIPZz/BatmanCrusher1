using TMPro;
using UnityEngine;

public class ChestRewardCount : MonoBehaviour
{
    [SerializeField] private ChestStorage _chestStorage;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _chestStorage.AllStuffAchieved += ShowReachedChests;
    }

    private void OnDisable()
    {
        _chestStorage.AllStuffAchieved -= ShowReachedChests;
    }

    private void ShowReachedChests(int allChests)
    {
        //_text.text = allChests.ToString();
    }
}