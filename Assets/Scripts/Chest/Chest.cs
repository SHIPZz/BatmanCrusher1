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

        _collider.size = new Vector3(0.7493408f, 0.7046234f, 0.5347263f);
        _collider.center = new Vector3(0.01330485f, 0.3809601f, -0.01462798f);
        _collider.isTrigger = true;
        gameObject.layer = 3;
        
        if (gameObject.GetComponent<Target>() == null)
            gameObject.AddComponent<Target>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Player player))
            return;
        
        _animator.SetTrigger(IsOpened);

        EffectPlayed?.Invoke();
        Achieved?.Invoke(this);
        GetComponent<Collider>().enabled = false;
        this.SetActive(gameObject, false, 1.5f);
    }
}