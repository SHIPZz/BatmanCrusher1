using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    public static event Action<Chest> Achieved;

    public event Action EffectPlayed;

    private static readonly int IsOpened = Animator.StringToHash("IsOpened");
    private Animator _animator;
    private BoxCollider _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();

        _collider.size = new Vector3(1.13f, 1.11f, 0.78f);
        _collider.isTrigger = true;
        if (gameObject.GetComponent<Target>() == null)
            gameObject.AddComponent<Target>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger(IsOpened);

        EffectPlayed?.Invoke();
        Achieved?.Invoke(this);
        GetComponent<Collider>().enabled = false;
        this.SetActive(gameObject, false, 1.5f);
    }
}