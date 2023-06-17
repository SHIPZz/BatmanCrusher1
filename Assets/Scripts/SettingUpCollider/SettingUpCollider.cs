using UnityEngine;

public class SettingUpCollider : MonoBehaviour
{
    [SerializeField] private float _sizeX = 0.11f;
    [SerializeField] private float _sizeY = 0.2205388f;
    [SerializeField] private float _sizeZ = 4.15f;
    [SerializeField] private float _centerX = -0.03f;
    [SerializeField] private float _centerY = -3.758756e-09f;
    [SerializeField] private float _centerZ = 1.253863f;
    [SerializeField] private bool _isTrigger = true;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = _isTrigger;
        _collider.size = new Vector3(_sizeX, _sizeY, _sizeZ);
        _collider.center = new Vector3(_centerX, _centerY, _centerZ);
    }
}