using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShootHand : MonoBehaviour
{
    [SerializeField] private PlayerMovement _move;

    private Rigidbody _rigidbody;
    private bool _isDisabled;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_move == null || _move.IsGrappling == false)
            return;

        _rigidbody.velocity = _move.GrapplingVelocity;
    }
}