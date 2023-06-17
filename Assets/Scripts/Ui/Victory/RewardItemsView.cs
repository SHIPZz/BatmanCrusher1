using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RewardItemsView : MonoBehaviour
{
    [SerializeField] private RewardItemFactory _rewardItemFactory;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Hide()
    {
        // GameObject[] childs = GetComponentsInParent<GameObject>();
        //
        // foreach (var child in childs)
        //     child.SetActive(false);
    }

    public void Show(IRewardItem[] rewards)
    {
        foreach (var reward in rewards)
        {
            CreateRewardItem(reward);
        }
    }

    private void CreateRewardItem(IRewardItem reward)
    {
        RewardItemView rewardItem = _rewardItemFactory.CreateItemByType(reward.Type);
        rewardItem.Count = reward.Count;
        rewardItem.SetParent(_rectTransform);
    }
}
