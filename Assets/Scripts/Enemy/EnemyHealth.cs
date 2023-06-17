using System;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(DistanceChecker))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action<EnemyHealth> Dead;

    public event Action HitDamaged;

    private readonly float _lastAttackTime = 0.7f;

    private Health _health;
    private float _lastHitTime;
    private DistanceChecker _distanceChecker;
    private Collider _collider;

    public Health Health =>
        _health;

    private void Awake()
    {
        _distanceChecker = GetComponent<DistanceChecker>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger= true;
        _health = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        if (IsColliderEnabled() == false)
            return;

        if (IsTimeNotGone() && _lastHitTime != 0)
            return;

        _health.TakeDamage(damage);
        _lastHitTime = Time.time;

        if(_health.MaxValue == 0)
            Dead?.Invoke(this);

        HitDamaged?.Invoke();
    }

    private bool IsTimeNotGone() =>
        Time.time - _lastHitTime < _lastAttackTime;

    private bool IsColliderEnabled() =>
        _collider.enabled && _collider != null;
}