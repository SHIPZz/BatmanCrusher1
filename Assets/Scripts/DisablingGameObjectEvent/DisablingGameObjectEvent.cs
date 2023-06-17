using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisablingGameObjectEvent : MonoBehaviour
{
    [SerializeField] private ChestStorage _chestStorage;
    [SerializeField] private Button _claimButton;
    [SerializeField] private EnemyObjectSpawner[] _spawners;

    private Player _player;

    public List<EnemyObjectSpawner> EnemyObjectSpawners =>
        _spawners.ToList();

    private List<EnemyHealth> _enemies = new List<EnemyHealth>();

    private void Awake()
    {
        this.enabled = false;
    }

    private void Start()
    {
        _player.Dead += DisableAllObjects;
    }

    private void OnEnable()
    {
        _chestStorage.PlayerAllReached += DisableAllObjects;

        foreach (var spawner in _spawners)
            spawner.EnemyHealthSpawned += AddToList;

    }

    private void OnDisable()
    {
        _player.Dead -= DisableAllObjects;
        _chestStorage.PlayerAllReached -= DisableAllObjects;

        foreach (var spawner in _spawners)
            spawner.EnemyHealthSpawned -= AddToList;
    }

    public void SetPlayer(Player player) =>
        _player = player;
    
    private void AddToList(EnemyHealth enemyHealth) =>
        _enemies.Add(enemyHealth);

    private void DisableAllObjects()
    {
        var playerParent = _player.transform.parent.gameObject.transform.parent;
        playerParent.gameObject.SetActive(false);
        
        foreach (var enemy in _enemies)
        {
            if (enemy == null)
                continue;

            enemy.gameObject.SetActive(false);
        }
    }
}