using UnityEngine;

public class EnemyDestroyingHandler : MonoBehaviour
{
    [SerializeField] private EnemyQuantity _enemyQuantity;

    public int Count { get; private set; }

    private void OnEnable()
    {
        _enemyQuantity.Removed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        _enemyQuantity.Removed -= OnEnemyDestroyed;
    }

    public void SetCount(int count)
    {
        Count = count;
        print(Count);
    }

    private void OnEnemyDestroyed(int obj)
    {
        Count++;
        DataProvider.Instance.SaveEnemyCount(Count);
    }
}