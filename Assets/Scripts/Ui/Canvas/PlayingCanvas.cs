using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCanvas : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private CanvasGroup _canvasGroup;
    private GoldMoneyPaySystem _moneyPaySystem;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        // GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        _canvasGroup.alpha = 1;
    }

    private void Start()
    {
        _moneyPaySystem = DependencyContainer.Get<GoldMoneyPaySystem>();
        // _canvasGroup.DOFade(1, 0.5f).SetLoops(-1, LoopType.Yoyo);
        Wallet.Instance.MoneyEnough += _moneyPaySystem_IsMoneyEnough;
    }
    
    private void OnDisable()
    {
        Wallet.Instance.MoneyEnough -= _moneyPaySystem_IsMoneyEnough;
    }
    
    private void _moneyPaySystem_IsMoneyEnough(bool isEnough)
    {
        if (!isEnough)
            return;

        _canvasGroup.DOKill();
        _canvasGroup.MoveCanvasAlpha(0, 0.5f);
    }
}