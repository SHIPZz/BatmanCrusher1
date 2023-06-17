using TMPro;
using UnityEngine;

public class PlayingText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

        _text.Change();
    }
}