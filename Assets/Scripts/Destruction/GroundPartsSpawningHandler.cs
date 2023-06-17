using RayFire;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundPartsSpawningHandler : MonoBehaviour
{
    private const int HardCubeLayer = 15;

    [SerializeField] private List<Rigidbody> _parts = new List<Rigidbody>();
    private EnemyDestruction _enemyDestruction;

    public event Action PlatformDestroyed;

    private void Awake()
    {
        gameObject.layer = HardCubeLayer;
        GetComponent<Collider>().isTrigger = false;
        this.SetKinematic(_parts, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyDestruction enemyDestruction))
        {
            _enemyDestruction = enemyDestruction;
            _enemyDestruction.Destroyed += Demolish;
        }
    }

    public void Demolish(Transform position)
    {
        foreach (var part in _parts)
        {
            this.SetActive(part.gameObject, true, 0f);
            part.isKinematic = false;
            part.transform.position = _enemyDestruction.transform.position;
        }

        _enemyDestruction.Destroyed -= Demolish;
        this.SetActive(gameObject, false, 0f);
        PlatformDestroyed?.Invoke();
    }

    private void SetPartsPosition(Vector3 position)
    {
        foreach (var part in _parts)
        {
            part.transform.position = position;
        }
    }
}