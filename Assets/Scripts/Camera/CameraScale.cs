using System.Collections;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private readonly float _initalZoom = 60;
    private readonly float _speed = 40;
    private Coroutine _zoom;

    public static CameraScale Instance { get; private set; }

    private void Awake()
    {
        _camera.fieldOfView = _initalZoom;
        Instance = this;
    }

    public void StartZoom(float targetValue, float targetSecondValue)
    {
        if (_zoom != null)
            StopCoroutine(_zoom);

        _zoom = StartCoroutine(StartZoomCoroutine(targetValue, targetSecondValue));
    }

    private IEnumerator StartZoomCoroutine(float targetValue, float targetSecondValue)
    {
        while (_camera.fieldOfView != targetValue)
        {
            _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, targetValue, Time.deltaTime * _speed);

            yield return null;
        }

        if (_camera.fieldOfView == targetValue)
        {
            while (_camera.fieldOfView != targetSecondValue)
            {
                _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, targetSecondValue, Time.deltaTime * _speed);

                yield return null;
            }
        }
    }
}