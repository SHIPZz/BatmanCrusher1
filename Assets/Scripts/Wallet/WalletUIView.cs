using TMPro;
using UnityEngine;

public class WalletUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public void SetMoneyCount(int money) =>
        _text.text = money.ToString();
}