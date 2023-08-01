using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LastLevel : MonoBehaviour
    {
        [SerializeField] private ChestStorage _chestStorage;

        private void OnEnable()
        {
            _chestStorage.Achieved += AddMoney;
        }

        private void OnDisable()
        {
            _chestStorage.Achieved -= AddMoney;
        }

        private void AddMoney()
        {
            Wallet.Instance.AddMoney(100);
            DataProvider.Instance.SaveMoney();
        }
    }
}