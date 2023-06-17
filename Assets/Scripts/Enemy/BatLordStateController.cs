using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BatLordStateController : MonoBehaviour, IEnemyStateController  
    {
        private readonly float _attackDistance = 3f;
        private readonly float _followDistance = 5f;
        
        private Player _player;
        private EnemyAttacker _enemyAttacker;
        private EnemyFollowing _enemyFollowing;
        private PlayerSpawner _playerSpawner;

        private void Start()
        {
            _enemyAttacker = GetComponent<EnemyAttacker>();
            _enemyFollowing = GetComponent<EnemyFollowing>();
            GetComponent<NavMeshAgent>().enabled = false;
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
                _enemyFollowing.StopMove();
                _enemyAttacker.StartAttack(_player);
            }
            else if (distanceToPlayer <= _followDistance)
            {
                _enemyAttacker.StopAttack();
                _enemyFollowing.StartMove(_player.transform);
            }
        }

        public void SetPlayerSpawner(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerSpawner.Spawned += SetPlayer;
        }

        private void SetPlayer(Player player)
        {
            _player = player;
        }
    }
}