using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text.text = Wallet.Instance.GetMoney().ToString();
    }
}