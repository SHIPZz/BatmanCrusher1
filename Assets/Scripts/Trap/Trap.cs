using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private const int IgnoreRaycastLayer = 2;

    [SerializeField] private int _damage = 100;

    public event Action Hit;

    private void Awake()
    {
        gameObject.layer = IgnoreRaycastLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player health))
        {
            health.TakeDamage(_damage);
            Hit?.Invoke();
        }
    }
}