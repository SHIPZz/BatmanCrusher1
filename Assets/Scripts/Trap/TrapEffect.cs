using UnityEngine;

public class TrapEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _prickEffect;

    private Trap _spikeTrap;

    private void Awake()
    {
        _spikeTrap = GetComponent<Trap>();
    }

    private void OnEnable()
    {
        _spikeTrap.Hit += OnHit;
    }

    private void OnDisable()
    {
        _spikeTrap.Hit -= OnHit;
    }

    private void OnHit()
    {
        _prickEffect.Play();
    }
}