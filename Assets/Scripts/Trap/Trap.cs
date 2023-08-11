using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trap : MonoBehaviour
{
    private const int IgnoreRaycastLayer = 2;

    [SerializeField] private int _damage = 100;

    public event Action Hit;

    private void Awake()
    {
        gameObject.layer = IgnoreRaycastLayer;
        
        if (transform.parent != null)
            transform.parent.gameObject.layer = IgnoreRaycastLayer;
        
        GetComponent<Collider>().isTrigger = true;
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