using TMPro;
using UnityEngine;

public class WalletUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    ////[SerializeField] private DataProvider _provider;

    private void Awake()
    {
         _text.text = Wallet.Instance.GetMoney().ToString();
    }

    //private void Start()
    //{
    //    _text.text = Wallet.Instance.GetMoney().ToString();
    //}
}