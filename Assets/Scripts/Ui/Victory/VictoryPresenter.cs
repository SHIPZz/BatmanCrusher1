public class VictoryPresenter : IVictoryPresenter
{
    private VictoryModel _model;

    public VictoryPresenter(VictoryModel victoryModel)
    {
        _model = victoryModel;
        Rewards = CreateRewards(victoryModel);
    }

    public IRewardItem[] Rewards { get; }

    public void OnClaimButtonClicked()
    {
        Wallet.Instance.AddMoney(_model.GoldCount);
        DataProvider.Instance.SaveMoney();
    }

    public void OnRewardClicked()
    {
        Wallet.Instance.AddMoney(_model.GoldCount * Constant.MultiplierReward);
        DataProvider.Instance.SaveMoney();
        
    }

    private IRewardItem[] CreateRewards(VictoryModel victoryModel)
    {
        return new IRewardItem[]
        {
            new RewardItem(Constant.GoldRewardType, victoryModel.GoldCount),
            new RewardItem(Constant.ChestRewardType, victoryModel.ChestCount),
        };
    }
}