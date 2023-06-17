using System;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private const int KillDamage = 1000;

    public event Action Touched;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>().TakeDamage(KillDamage);

        Touched?.Invoke();
    }
}