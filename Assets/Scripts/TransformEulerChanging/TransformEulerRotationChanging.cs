using UnityEngine;

public class TransformEulerRotationChanging : MonoBehaviour
{
    [SerializeField] private float _eulerX = 13f;
    [SerializeField] private float _eulerY;
    [SerializeField] private float _eulerZ;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _transform.localRotation = Quaternion.Euler(_eulerX, _eulerY, _eulerZ);
    }
}