using UnityEngine;
using UnityEngine.Serialization;

public class CommonDestruction : MonoBehaviour
{
    private const int DestroyDamage = 1000;

    // [SerializeField] private GroundPartsSpawningHandler groundPartsSpawningHandler;

    // private IDamageable _enemyHealth;
    // private EnemyDestruction _enemyDestruction;
    //
    // private void Awake()
    // {
    //     _enemyHealth = GetComponent<EnemyHealth>();
    //     _enemyDestruction= GetComponent<EnemyDestruction>();
    // }
    //
    // private void OnEnable()
    // {
    //     // groundPartsSpawningHandler.PlatformDestroyed += OnPlatformDestroyed;
    //     _enemyDestruction.Destroyed += OnEnemyDestroyed;
    // }
    //
    // private void OnDisable()
    // {
    //     // groundPartsSpawningHandler.PlatformDestroyed -= OnPlatformDestroyed;
    //     _enemyDestruction.Destroyed -= OnEnemyDestroyed;
    // }
    //
    // private void OnPlatformDestroyed() =>
    //     _enemyHealth.TakeDamage(DestroyDamage);

    // private void OnEnemyDestroyed(Transform transform) =>
    //     // groundPartsSpawningHandler.Demolish(transform);
}