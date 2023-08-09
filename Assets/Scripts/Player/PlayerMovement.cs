using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float ActiveDistance = 0.15f;

    [SerializeField] private GrapplingHook _hook;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _speedStep = 0.01f;

    private bool _isGrappling = false;
    private Vector3 _grapplingVelocity;
    private Coroutine _moveCoroutine;
    private bool _isMoved;
    private float _initalSpeed;


    public bool IsGrappling => _isGrappling;

    public Vector3 GrapplingVelocity => _grapplingVelocity;

    private void OnEnable()
    {
        _hook.Grappled += OnGrappled;
        _initalSpeed = _speed;
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
        {
            _isMoved = false;
            _speed = 3.5f;
            StopCoroutine(_moveCoroutine);
        }

        _isMoved = true;
        _moveCoroutine = StartCoroutine(MovePosition(point));
    }

    private IEnumerator MovePosition(Vector3 point)
    {
        if (!_isMoved) 
            yield break;
        
        while (Vector3.Distance(_rigidBody.position, point) > ActiveDistance)
        {
            _speed += _speedStep;

            if (_speed > 7)
                _speed = 7;

            var force = (point - _rigidBody.position) * _speed;
            _isGrappling = true;

            // Debug.Log(_speed);
            _grapplingVelocity = force;

            yield return new WaitForFixedUpdate();
        }

        _isGrappling = false;
        _grapplingVelocity = Vector3.zero;
        _isMoved = false;
    }
}