using System;
using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    private const int IgnoreRaycastLayer = 2;

    private EnemyDestruction _enemy;

    public event Action<Player> TriggerEntered;

    public event Action TriggerExited;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        _enemy = GetComponentInParent<EnemyDestruction>();
        gameObject.layer = IgnoreRaycastLayer;
    }

    private void OnEnable()
    {
        _enemy.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        _enemy.Destroyed -= OnDestroyed;
    }

    private void Start()
    {
        transform.SetParent(null);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExited?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {          
            TriggerEntered?.Invoke(player);
        }
    }

    private void OnDestroyed(Transform transform) =>
        this.SetActive(gameObject, false, 0);
}