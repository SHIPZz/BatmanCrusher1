using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceChecker), typeof(EnemyAnimator))]
public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    private readonly float _delay = 1f;

    private Tweener _tween;
    private Coroutine _rotation;
    private EnemyAnimator _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public void StartMove(UnityEngine.Transform target)
    {
        StartRotation(target);
        
        Chase(target);
    }

    public void StopMove()
    {
        _enemyAnimator.StopWalk();
    }

    private void Chase(UnityEngine.Transform target)
    {
        _tween.Kill();
        _tween = transform.DOMoveX(target.position.x, _delay);
        _enemyAnimator.StartWalk();
    }

    private void StartRotation(UnityEngine.Transform target)
    {
        if (_rotation != null)
            StopCoroutine(_rotation);

        _rotation = StartCoroutine(RotateCoroutine(target));
    }

    private IEnumerator RotateCoroutine(UnityEngine.Transform player)
    {
        while (transform.rotation != player.transform.rotation)
        {
            transform.LookAtXZ(player.transform.position, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}