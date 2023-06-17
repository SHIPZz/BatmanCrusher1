using UnityEngine;

public class LavaEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _effects;
    [SerializeField] private AudioSource _audio;

    private Lava _lava;

    private void Awake()
    {
        _lava = GetComponent<Lava>();
    }

    private void OnEnable()
    {
        _lava.Touched += OnTouched;
    }

    private void OnDisable()
    {
        _lava.Touched -= OnTouched;
    }

    private void OnTouched()
    {
        foreach (var effect in _effects)
        {
            effect.Play();
            _audio.Play();
        }
    }
}