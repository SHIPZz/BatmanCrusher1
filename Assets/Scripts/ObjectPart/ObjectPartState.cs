using UnityEngine;

public class ObjectPartState : MonoBehaviour
{
    [SerializeField] private EnemyDestruction _enemy;
    [SerializeField] private GameObject[] _newParts;
    [SerializeField] private float _height;

    private readonly float _delay = 7f;

    private void Awake()
    {
        this.SetActive(_newParts, false, 0);
    }

    private void OnEnable()
    {
        _enemy.Destroyed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        _enemy.Destroyed -= OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(Transform obj)
    {
        this.SetActive(_newParts, true, 0);
        SetNewPartsPosition(obj);

        this.SetActive(_newParts, false, _delay);
    }

    private void SetNewPartsPosition(Transform enemyPosition)
    {
        for (int i = 0; i < _newParts.Length; i++)
        {
            _newParts[i].transform.position = enemyPosition.position + new Vector3(0, _height, 0);
        }
    }
}