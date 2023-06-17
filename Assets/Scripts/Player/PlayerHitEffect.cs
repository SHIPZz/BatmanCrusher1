using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _hitEffects;

    private readonly float _offset = 2;
    private System.Random _random = new System.Random();
    private Player _player;
    private ParticleSystem _currentEffect;
    private Coroutine _effectCoroutine;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void LateUpdate()
    {
        if (_currentEffect != null)
            _currentEffect.transform.position = transform.position + Vector3.up + Vector3.right * _offset;
    }

    private void OnEnable()
    {
        _player.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _player.Damaged -= OnDamaged;
    }

    private void OnDamaged()
    {
        if (_effectCoroutine != null)
            StopCoroutine(_effectCoroutine);

        _effectCoroutine = StartCoroutine(PlayEffectCoroutine());
    }

    private ParticleSystem GetRandomEffect()
    {
        int randomNumber = _random.Next(0, _hitEffects.Length);

        return _hitEffects[randomNumber];
    }

    private IEnumerator PlayEffectCoroutine()
    {
        _currentEffect = GetRandomEffect();
        _currentEffect.Play();
        yield return null;
    }
}