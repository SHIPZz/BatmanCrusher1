using UnityEngine;
using UnityEngine.UI;

public class VictoryView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _claimButton;
    [SerializeField] private PlayingAdvertisingHandler _playingAdvertisingHandler;
    [SerializeField] private RewardItemsView _rewardItemView;

    private IVictoryPresenter _presenter;

    public void Show(IVictoryPresenter presenter)
    {
        gameObject.SetActive(true);
        _presenter = presenter;
        AddListeners();
        _rewardItemView.Show(presenter.Rewards);
        _canvasGroup.MoveCanvasAlpha(1, 1f);
    }

    private void Hide()
    {
        _rewardItemView.Hide();
        RemoveListeners();
        _canvasGroup.MoveCanvasAlpha(0, 1f);
        this.SetActive(gameObject, false, 1f);
    }

    private void AddListeners()
    {
        _claimButton.onClick.AddListener(OnClaimButtonClicked);
        _playingAdvertisingHandler.RewardedCallbackPlayed += OnRewardButtonClicked;
    }

    private void RemoveListeners()
    {
        _playingAdvertisingHandler.RewardedCallbackPlayed -= OnRewardButtonClicked;
        _claimButton.onClick.RemoveListener(OnClaimButtonClicked);
    }

    private void OnRewardButtonClicked()
    {
        _presenter.OnRewardClicked();
        Hide();
    }

    private void OnClaimButtonClicked()
    {
        _presenter.OnClaimButtonClicked();
        Hide();
    }
}