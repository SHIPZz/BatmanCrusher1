using TMPro;
using UnityEngine;

public class MoneyRewardCount : MonoBehaviour
{
    /*[SerializeField] private RewardSystem _rewardSystem;*/
    [SerializeField] private TextMeshProUGUI _text;

    public int Count { get; private set; }

    private void OnEnable()
    {
        /*_rewardSystem.MoneyAdded += ShowRewardedMoney;*/
    }

    private void OnDisable()
    {
        /*_rewardSystem.MoneyAdded -= ShowRewardedMoney;*/
    }

    private void ShowRewardedMoney(int obj)
    {
        Count += obj;
        _text.text = Count.ToString();
    }
}