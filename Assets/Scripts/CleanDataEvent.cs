using UnityEngine;
using UnityEngine.UI;

public class CleanDataEvent : MonoBehaviour
{
    [SerializeField] private Button _cleanButton;

    private void OnEnable()
    {
        _cleanButton.onClick.AddListener(Clean);
    }

    private void OnDisable()
    {
        _cleanButton.onClick.RemoveListener(Clean);
    }

    private void Clean() =>
        DataProvider.Instance.ClearData();
}