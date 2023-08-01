using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get;  set; }
    [field: SerializeField] public int InitialValue { get; private set; }

    public event Action<float> ValueChanged;
    public event Action ValueZeroReached;
    public event Action<float> Recovered;
    
    public void TakeDamage(float damage)
    {
        MaxValue = Mathf.Clamp(MaxValue - (int)damage, 0, MaxValue);

        if (MaxValue == 0)
        {
            ValueZeroReached?.Invoke();
        }

        ValueChanged?.Invoke(MaxValue);
    }

    public void RecoverHealth()
    {
        MaxValue = InitialValue;

        Recovered?.Invoke(InitialValue);
    }
}