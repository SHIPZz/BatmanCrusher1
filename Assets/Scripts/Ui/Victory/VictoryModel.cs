public class VictoryModel
{
    public VictoryModel(int chestCount)
    {
        ChestCount = chestCount;
        GoldCount = chestCount * Constant.Reward;
    }

    public int ChestCount { get; }
    public int GoldCount { get; }
}