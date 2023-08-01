using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float ActiveDistance = 0.15f;

    [SerializeField] private GrapplingHook _hook;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidBody;

    private bool _isGrappling = false;
    private Vector3 _grapplingVelocity;
    private Coroutine _moveCoroutine;

    public bool IsGrappling => _isGrappling;
    
    public Vector3 GrapplingVelocity => _grapplingVelocity;

    private void OnEnable()
    {
        _hook.Grappled += OnGrappled;
    }

    private void OnDisable()
    {
        _hook.Grappled -= OnGrappled;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidBody.velocity = velocity;
        _isGrappling = false;
    }

    private void OnGrappled(Vector3 point)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MovePosition(point));
    }

    private IEnumerator MovePosition(Vector3 point)
    {
        while (Vector3.Distance(_rigidBody.position, point) > ActiveDistance)
        {
            var force = (point - _rigidBody.position).normalized * _speed;
            _isGrappling = true;
            
            _grapplingVelocity = force;

            yield return new WaitForFixedUpdate();
        }

        _isGrappling = false;
        _grapplingVelocity = Vector3.zero;
    }
}