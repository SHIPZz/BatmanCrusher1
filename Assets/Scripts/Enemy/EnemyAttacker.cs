using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    private EnemyAnimator _enemyAnimator;
    private Player _player;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public void OnGaveDamage()
    {
        _player.TakeDamage(_damage);
    }

    public void StartAttack(Player player)
    {
        _player = player;
        _enemyAnimator.PlayAttack();
    }

    public void StopAttack()
    {
        _enemyAnimator.StopAttack();
    }
}