using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectAnimation : MonoBehaviour
{
    [SerializeField] private float _delay = 4f;

    private VisualEffect _visualEffect;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _visualEffect = GetComponent<VisualEffect>();
        StartCoroutine(StartFireCoroutine());
        _visualEffect.Play();
    }

    private IEnumerator StartFireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            _visualEffect.enabled = !_visualEffect.enabled;
            _collider.enabled = !_collider.enabled;
        }
    }
}