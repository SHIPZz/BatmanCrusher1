using System.Collections;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private ParticleSystem _angryEffect;
    [SerializeField] private AudioSource _audioSource;

    private readonly WaitForSeconds _firstDelay = new WaitForSeconds(0.5f);

    private Coroutine _delayDestroy;
    private EnemyHealth _enemyHealth;
    private bool _isBlocked
        ;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        _enemyHealth.HitDamaged += OnHitDamaged;
        _enemyHealth.Dead += BlockEffect;
    }

    private void OnDisable()
    {
        _enemyHealth.HitDamaged -= OnHitDamaged;
        _enemyHealth.Dead -= BlockEffect;
    }

    private void BlockEffect(EnemyHealth obj) => 
        _isBlocked = true;

    private void OnHitDamaged()
    {
        if(_isBlocked)
            return;
        
        if (_delayDestroy != null)
            StopCoroutine(_delayDestroy);

        _delayDestroy = StartCoroutine(TurnOffWithDelay());

        _bloodEffect.Play();
        _audioSource.Play();
    }

    private IEnumerator TurnOffWithDelay()
    {
        _angryEffect.Play();
        yield return _firstDelay;
        _angryEffect.gameObject.SetActive(false);
    }
}