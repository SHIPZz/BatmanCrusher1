using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class NotEnoughMoneyCanvasController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;

    private GoldMoneyPaySystem _moneyPaySystem;

    private async void Start()
    {
        await DelayingCoroutine();
    }

    private void OnEnable()
    {
        if (_moneyPaySystem != null)
        {
            _moneyPaySystem.IsMoneyEnough += IsMoneyEnough;
        }
    }

    private void OnDisable()
    {
        if (_moneyPaySystem != null)
        {
            _moneyPaySystem.IsMoneyEnough -= IsMoneyEnough;
        }
    }

    private void IsMoneyEnough(bool obj)
    {
        if (!obj)
        {
            _canvas.gameObject.SetActive(true);
            _canvas.MoveCanvasAlpha(1, 1);
        }
    }

    private async Task DelayingCoroutine()
    {
        _moneyPaySystem = await GetGoldMoneyPaySystemAsync();

        if (_moneyPaySystem != null)
        {
            _moneyPaySystem.IsMoneyEnough += IsMoneyEnough;
        }
    }

    private async Task<GoldMoneyPaySystem> GetGoldMoneyPaySystemAsync()
    {
        GoldMoneyPaySystem moneyPaySystem = null;

        while (moneyPaySystem == null)
        {
            moneyPaySystem = DependencyContainer.Get<GoldMoneyPaySystem>();
            await Task.Yield();
        }

        _canvas.gameObject.SetActive(true);
        return moneyPaySystem;
    }
}
