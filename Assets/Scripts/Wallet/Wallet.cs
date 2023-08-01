using System;
using System.Diagnostics;

public class Wallet
{
    private const int MaxMoney = 45000;

    public event Action<bool> MoneyEnough;

    public event Action<int> MoneyLoaded;

    public event Action<int> MoneyAdded;

    private static Wallet _instance;

    public static Wallet Instance => _instance ??= new Wallet();

    private int _money;

    public void AddMoney(int reward)
    {
        if (_money + reward > MaxMoney)
            return;

        _money += reward;


        MoneyAdded?.Invoke(_money);
    }

    public bool TryRemoveMoney(int amount)
    {
        if (IsMoneyEnough(amount) == false)
        {
            MoneyEnough?.Invoke(false);
            return false;
        }

        _money = Math.Clamp(_money - amount, 0, MaxMoney);
        DataProvider.Instance.SaveMoney();
        MoneyEnough?.Invoke(true);
        return true;
    }

    public int GetMoney() =>
        _money;

    public void LoadMoney(int money) =>
        _money = money;

    private bool IsMoneyEnough(int fine) =>
        _money >= fine;

    private bool IsMoneyNull() =>
        _money == 0;
}