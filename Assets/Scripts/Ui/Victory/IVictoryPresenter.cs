public interface IVictoryPresenter
{
    IRewardItem[] Rewards { get; }
    void OnClaimButtonClicked();
    void OnRewardClicked();
}