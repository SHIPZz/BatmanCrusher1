using UnityEngine;

public class RewardItemFactory : MonoBehaviour
{
    [SerializeField] private RewardItemView _chestRewardItem;
    [SerializeField] private RewardItemView _goldRewardItem;

    public RewardItemView CreateChest()
    {
        RewardItemView chestItem = Instantiate(_chestRewardItem);

        return chestItem;
    }

    public RewardItemView CreateItemByType(string type)
    {
        switch (type)
        {
            case Constant.ChestRewardType:
                return CreateChest();

            case Constant.GoldRewardType:
                return CreateGold();

            default:
                return null;
        }
    }

    public RewardItemView CreateGold()
    {
        RewardItemView goldItem = Instantiate(_goldRewardItem);

        return goldItem;
    }
}