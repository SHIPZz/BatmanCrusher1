using System;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyFollowing), typeof(EnemyDestruction))]
public class EnemyStateController : MonoBehaviour, IEnemyStateController
{
    private const int IgnoreRaycastLayer = 2;
    
    private readonly float _attackDistance = 2.5f;
    private readonly float _followDistance = 7f;
    
    private EnemyAttacker _enemyAttacker;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private PlayerSpawner _playerSpawner;

    private void Awake()
    {
        gameObject.layer = IgnoreRaycastLayer;
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        GetComponent<Animator>().enabled = true;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ConfigureNavMeshAgent();
    }

    private void OnDisable()
    {
        _playerSpawner.Spawned -= SetPlayer;
    }

    private void Update()
    {
        if (_player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= _attackDistance)
        {
            _enemyAttacker.StartAttack(_player);
        }
        else if (distanceToPlayer > _attackDistance && distanceToPlayer <= _followDistance)
        {
            _enemyAttacker.StopAttack();
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }

    private void ConfigureNavMeshAgent()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.acceleration = 30;
        _navMeshAgent.angularSpeed = 150;
        _navMeshAgent.speed = 7;
    }

    public void SetPlayer(Player player) =>
        _player = player;

    public void SetPlayerSpawner(PlayerSpawner playerSpawner)
    {
        _playerSpawner = playerSpawner;
        _playerSpawner.Spawned += SetPlayer;
    }
}