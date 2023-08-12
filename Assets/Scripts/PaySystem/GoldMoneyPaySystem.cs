using System;
using UnityEngine;

public class GoldMoneyPaySystem
{
    public event Action<bool> IsMoneyEnough;

    public bool TryBuyCharacter(int money)
    {
        if (!Wallet.Instance.TryRemoveMoney(money))
        {
            IsMoneyEnough?.Invoke(false);
            return false;
        }

        IsMoneyEnough?.Invoke(true);
        return true;
    }
}