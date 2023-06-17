using DG.Tweening;
using UnityEngine;

public class PatrolZoneConfigurator : MonoBehaviour
{
    [SerializeField] private float _scaleX;
    [SerializeField] private float _positionX;

    private EnemyObjectSpawner _objectSpawner;

    private void Awake()
    {
        _objectSpawner = GetComponent<EnemyObjectSpawner>();
    }

    private void OnEnable()
    {
        _objectSpawner.PatrolZoneSpawned += Configure;
    }

    private void OnDisable()
    {
        _objectSpawner.PatrolZoneSpawned -= Configure;
    }

    private void Configure(PatrolZone patrolZone)
    {
        if (_scaleX != 0)
            patrolZone.transform.DOScale(new Vector3(_scaleX, patrolZone.transform.lossyScale.y, patrolZone.transform.lossyScale.y), 0);

        if (_positionX != 0)
            patrolZone.transform.DOLocalMoveX(_positionX, 0);
    }
}