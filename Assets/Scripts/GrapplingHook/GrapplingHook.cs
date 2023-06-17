using System;
using UnityEngine;

[RequireComponent(typeof(HookRenderer))]
public class GrapplingHook : MonoBehaviour
{
    public const int HardCubeLayerMask = 15;

    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _web;

    public event Action<Vector3> Grappled;

    private readonly float _lastCreatedTime = 0.4f;

    private HookRenderer _hookRenderer;
    private Camera _camera;
    private float _elapsedTime = 0;

    private void Awake()
    {
        _hookRenderer = GetComponent<HookRenderer>();
    }

    public void SetCamera(Camera camera) =>
        _camera = camera;

    public void CreateHook()
    {
        RaycastHit hit = GetRaycastHitFromCamera();

        RaycastHit newHit = GetRaycastHitFromPlayer(hit);

        if (IsTimeNotGone(_elapsedTime))
            return;

        if (IsColliderNull(newHit.collider) == false && IsWrongLayerMask(newHit.collider, HardCubeLayerMask) == true)
            return;

        if (IsTargetVectorZero(newHit.point) == false)
        {
            _hookRenderer.DrawRope(newHit.point);
            _elapsedTime = Time.time;
            _web.SetActive(true);
            _web.transform.position = newHit.point;

            Grappled?.Invoke(newHit.point);
        }
    }

    public void DisableHook()
    {
        _web.SetActive(false);
        _hookRenderer.Disable();
    }

    private RaycastHit GetRaycastHitFromPlayer(RaycastHit hit)
    {
        var direction = (hit.point - transform.position).normalized;
        Ray ray = new(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit newHit, _maxDistance))
        {
            return newHit;
        }

        return new();
    }

    private RaycastHit GetRaycastHitFromCamera()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit;
        }

        return new();
    }

    private bool IsColliderNull(Collider collider) =>
        collider == null;

    private bool IsWrongLayerMask(Collider collider, int layerMask) =>
        collider.gameObject.layer == layerMask;

    private bool IsTargetVectorZero(Vector3 target) =>
        target == Vector3.zero;

    private bool IsTimeNotGone(float elapsedTime) =>
        Time.time - elapsedTime < _lastCreatedTime;
}