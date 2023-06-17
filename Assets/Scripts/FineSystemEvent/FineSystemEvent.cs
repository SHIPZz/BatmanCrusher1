using System;
using UnityEngine;

public class FineSystemEvent : MonoBehaviour
{
    //[SerializeField] private ClaimButton _claimButton;
    //[SerializeField] private HomeButton _homeButton;
    //[SerializeField] private DataProvider _dataProvider;

    //public event Action IsMoneyEnough;

    //public event Action IsMoneyNotEnough;

    //private readonly int _value = 500;

    //private void OnEnable()
    //{
    //    _claimButton.onClick.AddListener(RemoveMoney);
    //    _homeButton.onClick.AddListener(RemoveMoney);
    //}

    //private void OnDisable()
    //{
    //    _homeButton.onClick.RemoveListener(RemoveMoney);
    //    _claimButton.onClick.RemoveListener(RemoveMoney);
    //}

    //private void RemoveMoney()
    //{
    //    if (Wallet.Instance.TryRemoveMoney(_value) == true)
    //    {
    //        _dataProvider.SaveMoney();
    //        IsMoneyEnough?.Invoke();
    //    }
    //    else
    //    {
    //        IsMoneyNotEnough?.Invoke();
    //    }
    //}
}