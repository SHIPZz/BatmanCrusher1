using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using InsaneSystems.HealthbarsKit.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObjectSpawner : MonoBehaviour
{
    [SerializeField] private ObjectTypeId _objectTypeId;
    [SerializeField] private int _health;

    public event Action<EnemyObjectSpawner> Destroyed;
    public event Action<PatrolZone> PatrolZoneSpawned;
    public event Action<EnemyHealth> EnemyHealthSpawned;
    public event Action<List<AudioSource>> AudiosReceived;

    private GameFactory _gameFactory;
    private EnemyDestruction _enemyDestruction;
    private Player _player;
    private PlayerSpawner _playerSpawner;

    private void Start()
    {
        _gameFactory = DependencyContainer.Get<GameFactory>();

        switch (_objectTypeId)
        {
            case ObjectTypeId.Shadow:
                Create(Constant.ShadowPrefab);
                break;

            case ObjectTypeId.BatLord:
                Create(Constant.BatLordPrefab);
                break;

            case ObjectTypeId.Shade:
                Create(Constant.ShadePrefab);
                break;

            case ObjectTypeId.DarkSpider:
                Create(Constant.DarkSpiderPrefab);
                break;

            case ObjectTypeId.ToadStool:
                Create(Constant.ToadStoolPrefab);
                break;
            
            case ObjectTypeId.PurpleSpider:
                Create(Constant.PurpleSpider);
                break;
        }
    }

    private void Disable(Transform transform)
    {
        _enemyDestruction.Destroyed -= Disable;
        Destroyed?.Invoke(this);
        Destroy(gameObject, 1.5f);
    }

    private void Create(string name)
    {
        GameObject prefab = _gameFactory.CreateObject(name);
        prefab.transform.SetParent(transform);

        prefab.transform.position = transform.position;
        GetEnemyDestruction(prefab);
        _enemyDestruction.Destroyed += Disable;
        
        PatrolZone patrolZone = GetPatrolZone(prefab);

        var enemyStateController = prefab.GetComponentInChildren<IEnemyStateController>();
        SetPlayerToEnemyStateController(enemyStateController, _playerSpawner);

        DisableNavMesh(false);

        List<AudioSource> sources = new List<AudioSource>();
        sources = _enemyDestruction.transform.GetComponentsInChildren<AudioSource>(true).ToList();

        var healthbarController = prefab.GetComponentInChildren<HealthbarsController>();
        healthbarController.SetParentTransform(transform);

        if (IsHealthSet(_health))
        {
            SetHealthValue(prefab);
        }

        AudiosReceived?.Invoke(sources);
        PatrolZoneSpawned?.Invoke(patrolZone);
        EnemyHealthSpawned?.Invoke(prefab.GetComponentInChildren<EnemyHealth>());
    }

    private void SetHealthValue(GameObject prefab)
    {
        var enemyHealth = prefab.GetComponentInChildren<EnemyHealth>();
        enemyHealth.Health.InitialValue = _health;
        enemyHealth.Health.MaxValue = _health;
    }

    private PatrolZone GetPatrolZone(GameObject prefab)
    {
        PatrolZone patrolZone = prefab.GetComponentInChildren<PatrolZone>();
        return patrolZone;
    }

    private void GetEnemyDestruction(GameObject prefab)
    {
        _enemyDestruction = prefab.GetComponentInChildren<EnemyDestruction>();
    }

    public void SetPlayerSpawner(PlayerSpawner playerSpawner) =>
        _playerSpawner = playerSpawner;

    private void SetPlayerToEnemyStateController(IEnemyStateController enemyStateController, 
        PlayerSpawner playerSpawner) =>
        enemyStateController.SetPlayerSpawner(playerSpawner);
    
    private bool IsHealthSet(int value) =>
        _health > 0;

    private void DisableNavMesh(bool isEnabled)
    {
        NavMeshAgent navMeshAgent = _enemyDestruction.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = isEnabled;
    }
}