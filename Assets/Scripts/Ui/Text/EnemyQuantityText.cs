using TMPro;
using UnityEngine;

public class EnemyQuantityText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private EnemyQuantity _enemyQuantity;

    private void Start()
    {
        _text.text = _enemyQuantity.GetList().Count.ToString();
        _enemyQuantity.Removed += OnRemoved;
    }

    private void OnDisable()
    {
        _enemyQuantity.Removed -= OnRemoved;
    }

    private void OnRemoved(int quantity)
    {
        _text.text = quantity.ToString();
    }
}