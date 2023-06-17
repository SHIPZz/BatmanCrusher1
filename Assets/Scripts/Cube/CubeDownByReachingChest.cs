using RayFire;
using System.Collections.Generic;
using UnityEngine;

public class CubeDownByReachingChest : MonoBehaviour
{
    [SerializeField] private Chest _chest;
    [SerializeField] private List<GameObject> _parts = new List<GameObject>();

    private void Awake()
    {
        SetActive(false);
        SetTransform();
    }

    private void OnEnable()
    {
        _chest.EffectPlayed += Enable;
    }

    private void OnDisable()
    {
        _chest.EffectPlayed -= Enable;
    }

    private void SetActive(bool isActive)
    {
        _parts.ForEach(p => p.SetActive(isActive));
    }

    private void SetTransform() =>
        _parts.ForEach(x => x.gameObject.transform.position = transform.position);

    private void Enable()
    {
        SetActive(true);
        gameObject.SetActive(false);
    }
}