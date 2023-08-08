using System;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private const int KillDamage = 1000;

    public event Action Touched;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            player.TakeDamage(KillDamage);

        Touched?.Invoke();
    }
}