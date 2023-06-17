using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    /*[SerializeField]*/ private Player _target;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        if(_target == null)
            return;
        
        Vector3 wantedPosition = _target.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void SetPlayer(Player player) =>
        _target = player;
}