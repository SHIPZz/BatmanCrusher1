using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IDamageable
{
    public const int CharacterLayer = 8;

    [SerializeField] private int _damage;

    public event Action Dead;

    public event Action Damaged;

    private Health _health;

    public Health Health =>
        _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        Physics.IgnoreLayerCollision(CharacterLayer, CharacterLayer, true);

        GetComponent<PhysicalObject>().EachBody(body =>
        {
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.drag = 3f;
            body.angularDrag = 0.5f;
        });
    }

    private void OnEnable()
    {
        _health.ValueZeroReached += OnHealthZeroReached;
    }

    private void OnDisable()
    {
        _health.ValueZeroReached -= OnHealthZeroReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        Damaged?.Invoke();
    }

    private void OnHealthZeroReached()
    {
        Dead?.Invoke();
    }
}