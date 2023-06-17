public class RewardItem : IRewardItem
{
    public RewardItem(string type, int count)
    {
        Type = type;
        Count = count;
    }

    public string Type { get; }

    public int Count { get; }
}