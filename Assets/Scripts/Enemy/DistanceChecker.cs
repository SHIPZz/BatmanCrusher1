using System;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    public event Action<Player> PlayerApproached;

    public event Action PlayerExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerApproached?.Invoke(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerExited?.Invoke();
    }
}