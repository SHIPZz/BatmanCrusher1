using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeRenderer : MonoBehaviour
{
    //[SerializeField] private UnityEngine.Transform _hand;
    //[SerializeField] private GrapplingHook _hook;
    //[SerializeField] private int _segmentsCount;
    //[SerializeField] private float _drawSpeed;
    //[SerializeField] private float _damper;
    //[SerializeField] private float _strength;
    //[SerializeField] private float _velocity;
    //[SerializeField] private float _waveCount;
    //[SerializeField] private float _waveHeight;
    //[SerializeField] private AnimationCurve _curve;

    //private Vector3 _currentHookPosition;
    //private SpringRope _spring;
    //private LineRenderer _lineRenderer;

    //private void Awake()
    //{
    //    _lineRenderer = GetComponent<LineRenderer>();
    //    _spring = new SpringRope(_strength, _damper, _velocity);
    //}

    //private void LateUpdate()
    //{
    //    if (_hook.HasGrappled == true)
    //    {
    //        _currentHookPosition = _hand.position;
    //        _spring.Reset();

    //        if (_lineRenderer.positionCount > 0)
    //            _lineRenderer.positionCount = 0;

    //        return;
    //    }

    //    DrawRope();
    //}

    //public void DrawRope()
    //{
    //    if (_lineRenderer.positionCount == 0)
    //    {
    //        _spring.ChangeVelocity();
    //        _lineRenderer.positionCount = _segmentsCount + 1;
    //    }

    //    //SetWidthRopeByDistance();

    //    _spring.Change(Time.deltaTime);

    //    CreateRopeLine();
    //}

    //private void CreateRopeLine()
    //{
    //    var pillarPoint = _hook.Hit;
    //    var hookPosition = _hand.position;

    //    if (pillarPoint.collider == null || hookPosition == null)
    //        return;

    //    var direction = Quaternion.LookRotation(
    //        (pillarPoint.point - hookPosition).normalized) * Vector3.up;

    //    _currentHookPosition = Vector3.Lerp(
    //        _currentHookPosition, pillarPoint.point, _drawSpeed * Time.deltaTime);

    //    CreateSegments(direction, hookPosition);
    //}

    //private void CreateSegments(Vector3 direction, Vector3 hookPosition)
    //{
    //    for (var i = 0; i < _segmentsCount+1; i++)
    //    {
    //        var delta = i / (float)_segmentsCount;

    //        _lineRenderer.SetPosition(i, Vector3.Lerp(
    //            hookPosition, _currentHookPosition, delta) + GetOffset(direction, delta));
    //    }
    //}

    //private Vector3 GetOffset(Vector3 direction, float delta)
    //{
    //    return direction * _waveHeight * Mathf.Sin(
    //        delta * _waveCount * Mathf.PI) * _spring.Value * _curve.Evaluate(delta);
    //}

    //private void SetWidthRopeByDistance()
    //{
    //    float distanceFromPoint = Vector3.Distance(transform.position, _hook.Hit.point);
    //    float distance = distanceFromPoint * 0.02f;

    //    if (distanceFromPoint < 11)
    //    {
    //        _lineRenderer.startWidth = 0.35f - distance;
    //        _lineRenderer.endWidth = 0.35f - distance;
    //    }
    //    else
    //    {
    //        _lineRenderer.startWidth = 0.13f;
    //        _lineRenderer.endWidth = 0.13f;
    //    }
    //}
}