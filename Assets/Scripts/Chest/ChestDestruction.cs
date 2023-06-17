using RayFire;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RayfireRigid))]
public class ChestDestruction : MonoBehaviour
{
    private readonly float _destroyDelay = 0.7f;

    private Coroutine _delay;
    private RayfireRigid _rayfireRigid;
    private Chest _chest;

    private void Awake()
    {
        _rayfireRigid = GetComponent<RayfireRigid>();
        _chest = GetComponent<Chest>();
    }

    private void OnEnable()
    {
        _chest.EffectPlayed += OnEffectPlayed;
    }

    private void OnDisable()
    {
        _chest.EffectPlayed -= OnEffectPlayed;
    }

    private void OnEffectPlayed()
    {
        if (_delay != null)
            StopCoroutine(_delay);

        _delay = StartCoroutine(DestroyCubeCoroutine());
    }

    private IEnumerator DestroyCubeCoroutine()
    {
        yield return new WaitForSeconds(_destroyDelay);
        _rayfireRigid.Demolish();
    }
}