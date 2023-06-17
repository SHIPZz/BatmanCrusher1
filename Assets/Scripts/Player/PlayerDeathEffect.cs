using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private Health _health;

    private void Awake()
    {
        _health= GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.ValueZeroReached += PlayEffect;
    }

    private void OnDisable()
    {
        _health.ValueZeroReached -= PlayEffect;
    }

    private void PlayEffect()
    {
        _effect.Play();
    }
}