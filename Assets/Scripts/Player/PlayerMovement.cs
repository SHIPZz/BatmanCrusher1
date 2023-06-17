using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float ActiveDistance = 0.15f;

    [SerializeField] private GrapplingHook _hook;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidBody;

    private bool _isGrapping = false;
    private Vector3 _grappingVelocity;
    private Coroutine _moveCoroutine;

    public bool IsGrappling => _isGrapping;
    public Vector3 GrapplingVelocity => _grappingVelocity;

    private void OnEnable()
    {
        _hook.Grappled += OnGrappled;
    }

    private void OnDisable()
    {
        _hook.Grappled -= OnGrappled;
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
            var force = (point - _rigidBody.position) * _speed;
            _isGrapping = true;
            _grappingVelocity = force;

            yield return new WaitForFixedUpdate();
        }

        _isGrapping = false;
        _grappingVelocity = Vector3.zero;
    }
}