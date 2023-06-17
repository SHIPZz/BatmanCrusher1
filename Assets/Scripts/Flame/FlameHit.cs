using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SettingUpCollider))]
public class FlameHit : MonoBehaviour
{
    private const int IgnoreRaycastLayer = 2;

    [SerializeField] private float _damage = 10;

    private void Awake()
    {       
        gameObject.layer = IgnoreRaycastLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            player.TakeDamage(_damage);
    }
}