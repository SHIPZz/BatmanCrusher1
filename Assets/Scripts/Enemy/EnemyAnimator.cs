using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour, IAttackAnimator, IAnimatorMover
{
    private static readonly int _isAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int _isWalking = Animator.StringToHash("IsWalking");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        _animator.SetBool(_isAttacking, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(_isAttacking, false);
    }

    public void StartWalk()
    {
        _animator.SetBool(_isWalking, true);
    }

    public void StopWalk()
    {
        _animator.SetBool(_isWalking, false);
    }
}