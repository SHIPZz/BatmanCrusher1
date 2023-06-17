using UnityEngine;

public class HookRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private float _drawSpeed;

    private LineRenderer _lineRenderer;
    private bool _isDrawing = false;
    private Vector3 _targetGrapplePosition;
    private Vector3 _currentGrapplePosition;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        if (_isDrawing)
        {
            _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _targetGrapplePosition, Time.deltaTime * _drawSpeed);

            _lineRenderer.SetPosition(0, _shootPoint.transform.position);
            _lineRenderer.SetPosition(1, _currentGrapplePosition);
        }
    }

    public void DrawRope(Vector3 grapplePosition)
    {
        _isDrawing = true;
        _lineRenderer.positionCount = 2;

        _targetGrapplePosition = grapplePosition;
        _currentGrapplePosition = _shootPoint.transform.position;
    }

    public void Disable()
    {
        _isDrawing = false;

        _lineRenderer.positionCount = 0;
    }
}