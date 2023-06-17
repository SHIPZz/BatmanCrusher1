using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RewardItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _count;
    private RectTransform _rectTransform;

    public int Count
    {
        get => _count;
        set
        {
            if (value < 0)
                value = 0;

            _count = value;
            _text.text = value.ToString();
        }
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetParent(RectTransform rectTransform)
    {
        _rectTransform.parent = rectTransform;
        _rectTransform.localScale = Vector3.one;
    }
}